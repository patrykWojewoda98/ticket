using System;
using Application.Dtos;
using Domain.Abstractions;
using MediatR;

namespace Application.Queries.UserQueries.LoginUser;

public class LoginUserQueryHandler : IRequestHandler<LoginUserQuery, UserDto?>
{
  private readonly IUserRepository _repository;
  private readonly IPasswordHasher _passwordHasher;
  private readonly IUnitOfWorkService _unitOfWork;

  public LoginUserQueryHandler(IUserRepository repository, IPasswordHasher passwordHasher, IUnitOfWorkService unitOfWork)
  {
    _repository = repository;
    _passwordHasher = passwordHasher;
    _unitOfWork = unitOfWork;
  }

  public async Task<UserDto?> Handle(LoginUserQuery request, CancellationToken cancellationToken)
  {
    var user = await _repository.GetByEmailAsync(request.Email, cancellationToken);
    if (user == null) return null;

    if (user.LockoutEnd.HasValue && user.LockoutEnd > DateTime.UtcNow)
    {
      throw new Exception($"Konto zostało zablokowane na 15 minut (do {user.LockoutEnd.Value.ToLocalTime():HH:mm}).");
    }

    var isValid = await _passwordHasher.VerifyAsync(request.Password, user.Password, cancellationToken);
    if (!isValid)
    {
      user.FailedLoginAttempts++;
      if (user.FailedLoginAttempts >= 5)
      {
        user.LockoutEnd = DateTime.UtcNow.AddMinutes(15);
      }
      _repository.UpdateEntity(user);
      await _unitOfWork.SaveChangesAsync(cancellationToken);
      return null;
    }

    if (user.FailedLoginAttempts > 0 || user.LockoutEnd.HasValue)
    {
      user.FailedLoginAttempts = 0;
      user.LockoutEnd = null;
      _repository.UpdateEntity(user);
      await _unitOfWork.SaveChangesAsync(cancellationToken);
    }

    return new UserDto
    {
      Id = user.Id,
      Email = user.Email,
      Name = user.Name,
      Role = user.Role,
      CompanyId = user.CompanyId,
      LockoutEnd = user.LockoutEnd
    };
  }
}

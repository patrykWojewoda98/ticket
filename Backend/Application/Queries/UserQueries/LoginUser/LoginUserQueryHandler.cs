using System;
using Application.Dtos;
using Domain.Abstractions;
using MediatR;

namespace Application.Queries.UserQueries.LoginUser;

public class LoginUserQueryHandler : IRequestHandler<LoginUserQuery, LoginResultDto?>
{
  private readonly IUserRepository _repository;
  private readonly IPasswordHasher _passwordHasher;

  public LoginUserQueryHandler(IUserRepository repository, IPasswordHasher passwordHasher)
  {
    _repository = repository;
    _passwordHasher = passwordHasher;
  }

  public async Task<LoginResultDto?> Handle(LoginUserQuery request, CancellationToken cancellationToken)
  {
    var user = await _repository.GetByIdAsync(request.Id, cancellationToken);
    if (user == null) return null;

    if (user.IsBlocked)
    {
      return new LoginResultDto { IsBlocked = true, Message = "Account is blocked" };
    }

    var isValid = await _passwordHasher.VerifyAsync(request.Password, user.Password, cancellationToken);
    if (!isValid)
    {
      await _repository.IncrementFailedAttemptsAsync(user.Id, cancellationToken);
      return null;
    }

    // successful login -> reset attempts
    await _repository.ResetFailedAttemptsAsync(user.Id, cancellationToken);

    var dto = new UserDto
    {
      Id = user.Id,
      Email = user.Email,
      Name = user.Name,
      Role = user.Role,
      CompanyId = user.CompanyId
    };

    return new LoginResultDto { IsBlocked = false, User = dto };
  }
}

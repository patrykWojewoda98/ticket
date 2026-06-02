using System;
using Application.Dtos;
using Domain.Abstractions;
using MediatR;

namespace Application.Commands.UserCommands.UnlockUser;

public class UnlockUserCommandHandler : IRequestHandler<UnlockUserCommand, UserDto?>
{
  private readonly IUserRepository _repository;
  private readonly IUnitOfWorkService _unitOfWork;

  public UnlockUserCommandHandler(IUserRepository repository, IUnitOfWorkService unitOfWork)
  {
    _repository = repository;
    _unitOfWork = unitOfWork;
  }

  public async Task<UserDto?> Handle(UnlockUserCommand request, CancellationToken cancellationToken)
  {
    var user = await _repository.GetByIdAsync(request.UserId, cancellationToken);
    if (user == null) return null;

    user.FailedLoginAttempts = 0;
    user.LockoutEnd = null;

    _repository.UpdateEntity(user);
    await _unitOfWork.SaveChangesAsync(cancellationToken);

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

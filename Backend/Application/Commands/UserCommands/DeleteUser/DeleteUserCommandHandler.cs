using System;
using Application.Dtos;
using Domain.Abstractions;
using MediatR;

namespace Application.Commands.UserCommands.DeleteUser;

public class DeleteUserCommandHandler : IRequestHandler<DeleteUserCommand, UserDto>
{
  private readonly IUserRepository _repository;
  private readonly IUnitOfWorkService _unitOfWork;

  public DeleteUserCommandHandler(IUserRepository repository, IUnitOfWorkService unitOfWork)
  {
    _repository = repository;
    _unitOfWork = unitOfWork;
  }

  public async Task<UserDto> Handle(DeleteUserCommand request, CancellationToken cancellationToken)
  {
    var user = await _repository.GetByIdAsync(request.UserId, cancellationToken);
    if (user == null) return null;

    var UserDto = new UserDto
    {
      CompanyId = user.CompanyId,
      Email = user.Email,
      EmailVerified = user.EmailVerified,
      Role = user.Role,
      Name = user.Name,
      Image = user.Image
    };

    _repository.DeleteEntity(user);
    await _unitOfWork.SaveChangesAsync(cancellationToken);
    return UserDto;
  }
}

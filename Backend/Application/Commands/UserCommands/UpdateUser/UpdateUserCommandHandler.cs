using System;
using Application.Dtos;
using Domain.Abstractions;
using MediatR;

namespace Application.Commands.UserCommands.UpdateUser;

public class UpdateUserCommandHandler : IRequestHandler<UpdateUserCommand, UserDto>
{
  private readonly IUserRepository _repository;
  private readonly IUnitOfWorkService _unitOfWork;

  public UpdateUserCommandHandler(IUserRepository repository, IUnitOfWorkService unitOfWork)
  {
    _repository = repository;
    _unitOfWork = unitOfWork;
  }
  public async Task<UserDto> Handle(UpdateUserCommand request, CancellationToken cancellationToken)
  {
    var user = await _repository.GetByIdAsync(request.UserId, cancellationToken);
    if (user == null) return null;

    user.Name = request.Name;
    user.Email = request.Email;
    user.Role = request.Role;
    user.CompanyId = request.CompanyId;
    user.Image = request.Image;
    user.EmailVerified = request.EmailVerified.Value;

    _repository.UpdateEntity(user);
    await _unitOfWork.SaveChangesAsync(cancellationToken);
    return new UserDto
    {
      Id = user.Id,
      Name = user.Name,
      Email = user.Email,
      Role = user.Role,
      CompanyId = user.CompanyId,
      Image = user.Image,
      EmailVerified = user.EmailVerified
    };
  }
}

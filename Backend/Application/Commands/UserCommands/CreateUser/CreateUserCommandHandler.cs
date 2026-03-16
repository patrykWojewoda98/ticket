using System;
using Application.Dtos;
using Domain.Abstractions;
using Domain.Entities;
using MediatR;

namespace Application.Commands.UserCommands.CreateUser;

public class CreateUserCommandHandler : IRequestHandler<CreateUserCommand, UserDto>
{
  private readonly IUserRepository _repository;
  private readonly IUnitOfWorkService _unitOfWork;

  public CreateUserCommandHandler(IUserRepository repository, IUnitOfWorkService unitOfWork)
  {
    _repository = repository;
    _unitOfWork = unitOfWork;
  }

  public async Task<UserDto> Handle(CreateUserCommand request, CancellationToken cancellationToken)
  {
    var user = new User
    {
      CompanyId = request.CompanyId,
      Email = request.Email,
      EmailVerified = request.EmailVerified,
      Role = request.Role,
      Name = request.Name,
      Image = request.Image
    };

    _repository.CreateEntity(user);
    await _unitOfWork.SaveChangesAsync(cancellationToken);
    return new UserDto
    {
      Id = user.Id,
      CompanyId = user.CompanyId,
      Email = user.Email,
      EmailVerified = user.EmailVerified,
      Role = user.Role,
      Name = user.Name,
      Image = user.Image
    };
  }
}


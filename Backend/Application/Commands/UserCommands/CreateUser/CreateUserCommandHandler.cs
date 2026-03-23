using System;
using Application.Dtos;
using Domain.Abstractions;
using Domain.Entities;
using MediatR;

namespace Application.Commands.UserCommands.CreateUser;

public class CreateUserCommandHandler : IRequestHandler<CreateUserCommand, UserDto>
{
  private readonly IUserRepository _repository;
  private readonly IPasswordHasher _passwordHasher;
  private readonly IUnitOfWorkService _unitOfWork;

  public CreateUserCommandHandler(IUserRepository repository, IPasswordHasher passwordHasher, IUnitOfWorkService unitOfWork)
  {
    _repository = repository;
    _passwordHasher = passwordHasher;
    _unitOfWork = unitOfWork;
  }

  public async Task<UserDto> Handle(CreateUserCommand request, CancellationToken cancellationToken)
  {
    var user = new User
    {
      CompanyId = request.CompanyId,
      Email = request.Email,
      Password = await _passwordHasher.HashAsync(request.Password, cancellationToken),
      Role = request.Role,
      Name = request.Name,
    };

    _repository.CreateEntity(user);
    await _unitOfWork.SaveChangesAsync(cancellationToken);
    return new UserDto
    {
      Id = user.Id,
      CompanyId = user.CompanyId,
      Email = user.Email,
      Role = user.Role,
      Name = user.Name,
    };
  }
}


using System;
using Application.Dtos;
using Domain.Abstractions;
using MediatR;

namespace Application.Queries.UserQueries.LoginUser;

public class GetUserByIdQueryHandler : IRequestHandler<LoginUserQuery, UserDto?>
{
  private readonly IUserRepository _repository;
  private readonly IPasswordHasher _passwordHasher;

  public GetUserByIdQueryHandler(IUserRepository repository, IPasswordHasher passwordHasher)
  {
    _repository = repository;
    _passwordHasher = passwordHasher;
  }

  public async Task<UserDto?> Handle(LoginUserQuery request, CancellationToken cancellationToken)
  {
    var user = await _repository.GetByIdAsync(request.Id, cancellationToken);
    if (user == null) return null;

    var isValid = await _passwordHasher.VerifyAsync(request.Password, user.Password, cancellationToken);
    if (!isValid) return null;

    return new UserDto
    {
      Id = user.Id,
      Email = user.Email,
      Name = user.Name,
      Role = user.Role,
      CompanyId = user.CompanyId
    };
  }
}

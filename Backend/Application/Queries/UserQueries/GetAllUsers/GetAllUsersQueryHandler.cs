using System;
using Application.Dtos;
using Domain.Abstractions;
using MediatR;

namespace Application.Queries.UserQueries.GetAllUsers;

public class GetAllUsersQueryHandler : IRequestHandler<GetAllUsersQuery, List<UserDto>>
{
  private readonly IUserRepository _repository;

  public GetAllUsersQueryHandler(IUserRepository repository)
  {
    _repository = repository;
  }

  public async Task<List<UserDto>> Handle(GetAllUsersQuery request, CancellationToken cancellationToken)
  {
    var users = await _repository.GetAllAsync();
    return users.Select(user => new UserDto
    {
      Id = user.Id,
      CompanyId = user.CompanyId,
      Email = user.Email,
      Password = user.Password,
      Role = user.Role,
      Name = user.Name,
    }).ToList();
  }
}

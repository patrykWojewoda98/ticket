using System;
using Application.Dtos;
using Domain.Abstractions;
using MediatR;

namespace Application.Queries.UserQueries.FindByRole;

public class FindByRoleQueryHandler : IRequestHandler<FindByRoleQuery, List<UserDto>>
{
  private readonly IUserRepository _repository;

  public FindByRoleQueryHandler(IUserRepository repository)
  {
    _repository = repository;
  }

  public async Task<List<UserDto>> Handle(FindByRoleQuery request, CancellationToken cancellationToken)
  {
    var users = await _repository.FindByRoleAsync(request.Role);
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

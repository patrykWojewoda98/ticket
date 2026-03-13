using System;
using Application.Dtos;
using Domain.Abstractions;
using MediatR;

namespace Application.Queries.UserQueries.SetUserRole;

public class SetUserRoleQueryHandler : IRequestHandler<SetUserRoleQuery, UserDto>
{
  private readonly IUserRepository _repository;

  public SetUserRoleQueryHandler(IUserRepository repository)
  {
    _repository = repository;
  }

  public async Task<UserDto> Handle(SetUserRoleQuery request, CancellationToken cancellationToken)
  {
    var user = await _repository.SetUserRoleAsync(request.UserId, request.Role);
    if (user == null) return null;

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

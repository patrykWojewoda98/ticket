using System;
using Application.Dtos;
using Domain.Abstractions;
using MediatR;

namespace Application.Queries.UserQueries.GetUserById;

public class GetUserByIdQueryHandler : IRequestHandler<GetUserByIdQuery, UserDto>
{
  private readonly IUserRepository _repository;

  public GetUserByIdQueryHandler(IUserRepository repository)
  {
    _repository = repository;
  }

  public async Task<UserDto> Handle(GetUserByIdQuery request, CancellationToken cancellationToken)
  {
    var user = await _repository.GetByIdAsync(request.Id);
    if (user == null) return null;

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

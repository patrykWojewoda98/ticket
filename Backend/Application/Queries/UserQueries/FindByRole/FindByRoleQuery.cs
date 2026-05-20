using System;
using Application.Dtos;
using MediatR;

namespace Application.Queries.UserQueries.FindByRole;

public record FindByRoleQuery(string Role) : IRequest<List<UserDto>>;

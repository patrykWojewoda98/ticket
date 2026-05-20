using System;
using Application.Dtos;
using MediatR;

namespace Application.Queries.UserQueries.SetUserRole;

public record SetUserRoleQuery(int UserId, string Role) : IRequest<UserDto>;

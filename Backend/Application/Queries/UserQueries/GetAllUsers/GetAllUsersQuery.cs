using System;
using Application.Dtos;
using MediatR;

namespace Application.Queries.UserQueries.GetAllUsers;

public record GetAllUsersQuery() : IRequest<List<UserDto>>;

using System;
using Application.Dtos;
using MediatR;

namespace Application.Queries.UserQueries.GetUserById;

public record GetUserByIdQuery(int Id) : IRequest<UserDto>;

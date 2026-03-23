using System;
using Application.Dtos;
using MediatR;

namespace Application.Queries.UserQueries.LoginUser;

public record LoginUserQuery(int Id, string Password) : IRequest<UserDto?>;

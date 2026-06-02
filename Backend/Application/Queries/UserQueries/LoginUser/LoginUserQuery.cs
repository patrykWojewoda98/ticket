using System;
using Application.Dtos;
using MediatR;

namespace Application.Queries.UserQueries.LoginUser;

public record LoginUserQuery(string Email, string Password) : IRequest<UserDto?>;

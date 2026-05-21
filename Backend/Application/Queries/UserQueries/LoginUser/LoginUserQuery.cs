using System;
using Application.Dtos;
using MediatR;

namespace Application.Queries.UserQueries.LoginUser;

<<<<<<< HEAD
public record LoginUserQuery(int Id, string Password) : IRequest<LoginResultDto?>;
=======
public record LoginUserQuery(int Id, string Password) : IRequest<UserDto?>;
>>>>>>> 8bdda2c58a129a22e9d27085a8ac580aa62d740e

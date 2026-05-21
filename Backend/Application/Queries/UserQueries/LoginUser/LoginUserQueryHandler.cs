using System;
using Application.Dtos;
using Domain.Abstractions;
using MediatR;

namespace Application.Queries.UserQueries.LoginUser;

<<<<<<< HEAD
public class LoginUserQueryHandler : IRequestHandler<LoginUserQuery, LoginResultDto?>
=======
public class GetUserByIdQueryHandler : IRequestHandler<LoginUserQuery, UserDto?>
>>>>>>> 8bdda2c58a129a22e9d27085a8ac580aa62d740e
{
  private readonly IUserRepository _repository;
  private readonly IPasswordHasher _passwordHasher;

<<<<<<< HEAD
  public LoginUserQueryHandler(IUserRepository repository, IPasswordHasher passwordHasher)
=======
  public GetUserByIdQueryHandler(IUserRepository repository, IPasswordHasher passwordHasher)
>>>>>>> 8bdda2c58a129a22e9d27085a8ac580aa62d740e
  {
    _repository = repository;
    _passwordHasher = passwordHasher;
  }

<<<<<<< HEAD
  public async Task<LoginResultDto?> Handle(LoginUserQuery request, CancellationToken cancellationToken)
=======
  public async Task<UserDto?> Handle(LoginUserQuery request, CancellationToken cancellationToken)
>>>>>>> 8bdda2c58a129a22e9d27085a8ac580aa62d740e
  {
    var user = await _repository.GetByIdAsync(request.Id, cancellationToken);
    if (user == null) return null;

<<<<<<< HEAD
    if (user.IsBlocked)
    {
      return new LoginResultDto { IsBlocked = true, Message = "Account is blocked" };
    }

    var isValid = await _passwordHasher.VerifyAsync(request.Password, user.Password, cancellationToken);
    if (!isValid)
    {
      await _repository.IncrementFailedAttemptsAsync(user.Id, cancellationToken);
      return null;
    }

    // successful login -> reset attempts
    await _repository.ResetFailedAttemptsAsync(user.Id, cancellationToken);

    var dto = new UserDto
=======
    var isValid = await _passwordHasher.VerifyAsync(request.Password, user.Password, cancellationToken);
    if (!isValid) return null;

    return new UserDto
>>>>>>> 8bdda2c58a129a22e9d27085a8ac580aa62d740e
    {
      Id = user.Id,
      Email = user.Email,
      Name = user.Name,
      Role = user.Role,
      CompanyId = user.CompanyId
    };
<<<<<<< HEAD

    return new LoginResultDto { IsBlocked = false, User = dto };
=======
>>>>>>> 8bdda2c58a129a22e9d27085a8ac580aa62d740e
  }
}

using System;
using Application.Dtos;
using Domain.Abstractions;
using Domain.Entities;
using MediatR;

namespace Application.Commands.AccountCommands.CreateAccount;

public class CreateAccountCommandHandler : IRequestHandler<CreateAccountCommand, AccountDto>
{
  private readonly IAccountRepository _repository;
  private readonly IUnitOfWorkService _unitOfWork;

  public CreateAccountCommandHandler(IAccountRepository repository, IUnitOfWorkService unitOfWork)
  {
    _repository = repository;
    _unitOfWork = unitOfWork;
  }

  public async Task<AccountDto> Handle(CreateAccountCommand request, CancellationToken cancellationToken)
  {
    var account = new Account
    {
      UserId = request.UserId,
      ProviderId = request.ProviderId,
      Password = request.Password,
      Scope = request.Scope,
      IdToken = request.IdToken,
      AccessToken = request.AccessToken,
      RefreshToken = request.RefreshToken,
      AccessTokenExpiresAt = request.AccessTokenExpiresAt,
      RefreshTokenExpiresAt = request.RefreshTokenExpiresAt
    };

    _repository.CreateEntity(account);
    await _unitOfWork.SaveChangesAsync(cancellationToken);
    return new AccountDto
    {
      Id = account.Id,
      UserId = account.UserId,
      ProviderId = account.ProviderId
    };
  }
}

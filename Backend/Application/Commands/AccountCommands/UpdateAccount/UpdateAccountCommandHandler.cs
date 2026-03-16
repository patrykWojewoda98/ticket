using System;
using Application.Dtos;
using Domain.Abstractions;
using MediatR;

namespace Application.Commands.AccountCommands.UpdateAccount;

public class UpdateAccountCommandHandler : IRequestHandler<UpdateAccountCommand, AccountDto>
{
  private readonly IAccountRepository _repository;
  private readonly IUnitOfWorkService _unitOfWork;

  public UpdateAccountCommandHandler(IAccountRepository repository, IUnitOfWorkService unitOfWork)
  {
    _repository = repository;
    _unitOfWork = unitOfWork;
  }
  public async Task<AccountDto> Handle(UpdateAccountCommand request, CancellationToken cancellationToken)
  {
    var account = await _repository.GetByIdAsync(request.AccountId, cancellationToken);
    if (account == null) return null;

    account.Scope = request.Scope;
    account.AccessToken = request.AccessToken;
    account.RefreshToken = request.RefreshToken;
    account.AccessTokenExpiresAt = request.AccessTokenExpiresAt;
    account.RefreshTokenExpiresAt = request.RefreshTokenExpiresAt;
    account.Password = request.Password;

    _repository.UpdateEntity(account);
    await _unitOfWork.SaveChangesAsync(cancellationToken);
    return new AccountDto
    {
      Id = account.Id,
      UserId = account.UserId,
      ProviderId = account.ProviderId,
    };
  }
}

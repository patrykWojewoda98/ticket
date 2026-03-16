using System;
using Application.Dtos;
using Domain.Abstractions;
using MediatR;

namespace Application.Commands.AccountCommands.DeleteAccount;

public class DeleteAccountCommandHandler : IRequestHandler<DeleteAccountCommand, AccountDto>
{
  private readonly IAccountRepository _repository;
  private readonly IUnitOfWorkService _unitOfWork;

  public DeleteAccountCommandHandler(IAccountRepository repository, IUnitOfWorkService unitOfWork)
  {
    _repository = repository;
    _unitOfWork = unitOfWork;
  }

  public async Task<AccountDto> Handle(DeleteAccountCommand request, CancellationToken cancellationToken)
  {
    var account = await _repository.GetByIdAsync(request.AccountId, cancellationToken);
    if (account == null) return null;

    var accountDto = new AccountDto
    {
      Id = account.Id,
      UserId = account.UserId,
      ProviderId = account.ProviderId
    };

    _repository.DeleteEntity(account);
    await _unitOfWork.SaveChangesAsync(cancellationToken);
    return accountDto;
  }
}

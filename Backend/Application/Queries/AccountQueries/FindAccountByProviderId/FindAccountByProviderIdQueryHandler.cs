using System;
using Application.Dtos;
using Domain.Abstractions;
using MediatR;

namespace Application.Queries.AccountQueries.FindAccountByProviderId;

public class FindAccountByProviderIdQueryHandler : IRequestHandler<FindAccountByProviderIdQuery, AccountDto?>
{
  private readonly IAccountRepository _repository;

  public FindAccountByProviderIdQueryHandler(IAccountRepository repository)
  {
    _repository = repository;
  }

  public async Task<AccountDto?> Handle(FindAccountByProviderIdQuery request, CancellationToken cancellationToken)
  {
    var account = await _repository.FindByProviderIdAsync(request.ProviderId, request.AccountId);
    if (account == null) return null;

    return new AccountDto
    {
      Id = account.Id,
      UserId = account.UserId,
      ProviderId = account.ProviderId,
      AccountId = account.AccountId,
    };
  }
}

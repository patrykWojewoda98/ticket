using System;
using Application.Dtos;
using Domain.Abstractions;
using MediatR;

namespace Application.Queries.AccountQueries.GetAllAccounts;

public class GetAllAccountsQueryHandler : IRequestHandler<GetAllAccountsQuery, List<AccountDto>>
{
  private readonly IAccountRepository _repository;

  public GetAllAccountsQueryHandler(IAccountRepository repository)
  {
    _repository = repository;
  }

  public async Task<List<AccountDto>> Handle(GetAllAccountsQuery request, CancellationToken cancellationToken)
  {
    var accounts = await _repository.GetAllAsync();
    return accounts.Select(account => new AccountDto
    {
      Id = account.Id,
      UserId = account.UserId,
      ProviderId = account.ProviderId,
      AccountId = account.AccountId
    }).ToList();
  }
}

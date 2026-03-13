using System;
using Application.Dtos;
using Domain.Abstractions;
using MediatR;

namespace Application.Queries.AccountQueries.FindAccountsByUserId;

public class FindAccountsByUserIdQueryHandler : IRequestHandler<FindAccountsByUserIdQuery, List<AccountDto>>
{
  private readonly IAccountRepository _repository;

  public FindAccountsByUserIdQueryHandler(IAccountRepository repository)
  {
    _repository = repository;
  }

  public async Task<List<AccountDto>> Handle(FindAccountsByUserIdQuery request, CancellationToken cancellationToken)
  {
    var accounts = await _repository.FindByUserIdAsync(request.UserId);
    return accounts.Select(account => new AccountDto
    {
      Id = account.Id,
      UserId = account.UserId,
      ProviderId = account.ProviderId,
      AccountId = account.AccountId,
    }).ToList();
  }
}

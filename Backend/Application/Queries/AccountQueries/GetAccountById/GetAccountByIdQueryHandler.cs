using System;
using Application.Dtos;
using Domain.Abstractions;
using MediatR;

namespace Application.Queries.AccountQueries.GetAccountById;

public class GetAccountByIdQueryHandler : IRequestHandler<GetAccountByIdQuery, AccountDto>
{
  private readonly IAccountRepository _repository;

  public GetAccountByIdQueryHandler(IAccountRepository repository)
  {
    _repository = repository;
  }

  public async Task<AccountDto> Handle(GetAccountByIdQuery request, CancellationToken cancellationToken)
  {
    var account = await _repository.GetByIdAsync(request.Id);
    if (account == null) return null;

    return new AccountDto
    {
      Id = account.Id,
      UserId = account.UserId,
      ProviderId = account.ProviderId,
      AccountId = account.AccountId
    };
  }
}

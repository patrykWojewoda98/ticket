using System;
using Application.Dtos;
using MediatR;

namespace Application.Queries.AccountQueries.GetAllAccounts;

public record GetAllAccountsQuery() : IRequest<List<AccountDto>>;

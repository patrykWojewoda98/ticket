using System;
using Application.Dtos;
using MediatR;

namespace Application.Queries.AccountQueries.FindAccountByProviderId;

public record FindAccountByProviderIdQuery(string ProviderId, int AccountId) : IRequest<AccountDto?>;

using System;
using Application.Dtos;
using MediatR;

namespace Application.Queries.AccountQueries.FindAccountByProviderId;

public record FindAccountByProviderIdQuery(int ProviderId, int AccountId) : IRequest<AccountDto?>;

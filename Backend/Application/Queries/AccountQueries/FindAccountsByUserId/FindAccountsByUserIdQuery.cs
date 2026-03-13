using System;
using Application.Dtos;
using MediatR;

namespace Application.Queries.AccountQueries.FindAccountsByUserId;

public record FindAccountsByUserIdQuery(int UserId) : IRequest<List<AccountDto>>;

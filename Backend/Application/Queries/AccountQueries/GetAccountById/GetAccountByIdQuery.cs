using System;
using Application.Dtos;
using MediatR;

namespace Application.Queries.AccountQueries.GetAccountById;

public record GetAccountByIdQuery(int Id) : IRequest<AccountDto>;

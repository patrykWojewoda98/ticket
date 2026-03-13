using System;
using Application.Dtos;
using MediatR;

namespace Application.Queries.CompanyQueries.FindByUserId;

public record FindByUserIdQuery(int UserId) : IRequest<List<CompanyDto>>;

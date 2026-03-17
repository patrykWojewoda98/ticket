using System;
using Application.Dtos;
using MediatR;

namespace Application.Queries.CompanyQueries.GetCompanyById;

public record GetCompanyByIdQuery(int Id) : IRequest<CompanyDto>;

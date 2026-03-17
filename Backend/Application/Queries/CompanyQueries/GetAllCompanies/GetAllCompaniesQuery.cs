using System;
using Application.Dtos;
using MediatR;

namespace Application.Queries.CompanyQueries.GetAllCompanies;

public record GetAllCompaniesQuery() : IRequest<List<CompanyDto>>;

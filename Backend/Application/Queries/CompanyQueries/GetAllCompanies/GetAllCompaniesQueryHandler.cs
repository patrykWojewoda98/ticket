using System;
using Application.Dtos;
using Domain.Abstractions;
using MediatR;

namespace Application.Queries.CompanyQueries.GetAllCompanies;

public class GetAllCompaniesQueryHandler : IRequestHandler<GetAllCompaniesQuery, List<CompanyDto>>
{
  private readonly ICompanyRepository _repository;

  public GetAllCompaniesQueryHandler(ICompanyRepository repository)
  {
    _repository = repository;
  }

  public async Task<List<CompanyDto>> Handle(GetAllCompaniesQuery request, CancellationToken cancellationToken)
  {
    var companies = await _repository.GetAllAsync();
    return companies.Select(company => new CompanyDto
    {
      Id = company.Id,
      UserId = company.UserId,
      Name = company.Name,
      Email = company.Email,
      PhoneNumber = company.PhoneNumber,
      Address = company.Address
    }).ToList();
  }
}

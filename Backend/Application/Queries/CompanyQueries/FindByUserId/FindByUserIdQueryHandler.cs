using System;
using Application.Dtos;
using Domain.Abstractions;
using MediatR;

namespace Application.Queries.CompanyQueries.FindByUserId;

public class FindByUserIdQueryHandler : IRequestHandler<FindByUserIdQuery, List<CompanyDto>>
{
  private readonly ICompanyRepository _repository;

  public FindByUserIdQueryHandler(ICompanyRepository repository)
  {
    _repository = repository;
  }

  public async Task<List<CompanyDto>> Handle(FindByUserIdQuery request, CancellationToken cancellationToken)
  {
    var companies = await _repository.FindByUserIdAsync(request.UserId);
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

using System;
using Application.Dtos;
using Domain.Abstractions;
using MediatR;

namespace Application.Queries.CompanyQueries.GetCompanyById;

public class GetCompanyByIdQueryHandler : IRequestHandler<GetCompanyByIdQuery, CompanyDto>
{
  private readonly ICompanyRepository _repository;

  public GetCompanyByIdQueryHandler(ICompanyRepository repository)
  {
    _repository = repository;
  }

  public async Task<CompanyDto> Handle(GetCompanyByIdQuery request, CancellationToken cancellationToken)
  {
    var company = await _repository.GetByIdAsync(request.Id);
    if (company == null) return null;

    return new CompanyDto
    {
      Id = company.Id,
      UserId = company.UserId,
      Name = company.Name,
      Email = company.Email,
      PhoneNumber = company.PhoneNumber,
      Address = company.Address
    };
  }
}

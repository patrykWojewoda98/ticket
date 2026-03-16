using System;
using Application.Dtos;
using Domain.Abstractions;
using MediatR;

namespace Application.Commands.CompanyCommands.UpdateCompany;

public class UpdateCompanyCommandHandler : IRequestHandler<UpdateCompanyCommand, CompanyDto>
{
  private readonly ICompanyRepository _repository;
  private readonly IUnitOfWorkService _unitOfWork;

  public UpdateCompanyCommandHandler(ICompanyRepository repository, IUnitOfWorkService unitOfWork)
  {
    _repository = repository;
    _unitOfWork = unitOfWork;
  }
  public async Task<CompanyDto> Handle(UpdateCompanyCommand request, CancellationToken cancellationToken)
  {
    var company = await _repository.GetByIdAsync(request.CompanyId, cancellationToken);
    if (company == null) return null;

    company.Name = request.Name;
    company.Email = request.Email;
    company.PhoneNumber = request.PhoneNumber;
    company.Address = request.Address;

    _repository.UpdateEntity(company);
    await _unitOfWork.SaveChangesAsync(cancellationToken);
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

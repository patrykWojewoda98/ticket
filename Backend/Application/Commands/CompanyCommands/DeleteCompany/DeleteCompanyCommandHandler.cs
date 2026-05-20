using System;
using Application.Dtos;
using Domain.Abstractions;
using MediatR;

namespace Application.Commands.CompanyCommands.DeleteCompany;

public class DeleteCompanyCommandHandler : IRequestHandler<DeleteCompanyCommand, CompanyDto>
{
  private readonly ICompanyRepository _repository;
  private readonly IUnitOfWorkService _unitOfWork;

  public DeleteCompanyCommandHandler(ICompanyRepository repository, IUnitOfWorkService unitOfWork)
  {
    _repository = repository;
    _unitOfWork = unitOfWork;
  }

  public async Task<CompanyDto> Handle(DeleteCompanyCommand request, CancellationToken cancellationToken)
  {
    var company = await _repository.GetByIdAsync(request.CompanyId, cancellationToken);
    if (company == null) return null;

    var CompanyDto = new CompanyDto
    {
      Id = company.Id,
      UserId = company.UserId,
      Name = company.Name,
      Email = company.Email,
      PhoneNumber = company.PhoneNumber,
      Address = company.Address
    };

    _repository.DeleteEntity(company);
    await _unitOfWork.SaveChangesAsync(cancellationToken);
    return CompanyDto;
  }
}

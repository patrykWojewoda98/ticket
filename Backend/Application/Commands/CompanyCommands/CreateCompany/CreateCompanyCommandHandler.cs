using System;
using Application.Dtos;
using Domain.Abstractions;
using Domain.Entities;
using MediatR;

namespace Application.Commands.CompanyCommands.CreateCompany;

public class CreateCompanyCommandHandler : IRequestHandler<CreateCompanyCommand, CompanyDto>
{
  private readonly ICompanyRepository _repository;
  private readonly IUnitOfWorkService _unitOfWork;

  public CreateCompanyCommandHandler(ICompanyRepository repository, IUnitOfWorkService unitOfWork)
  {
    _repository = repository;
    _unitOfWork = unitOfWork;
  }

  public async Task<CompanyDto> Handle(CreateCompanyCommand request, CancellationToken cancellationToken)
  {
    var company = new Company
    {
      UserId = request.UserId,
      Name = request.Name,
      Email = request.Email,
      PhoneNumber = request.PhoneNumber,
      Address = request.Address
    };

    _repository.CreateEntity(company);
    await _unitOfWork.SaveChangesAsync(cancellationToken);
    return new CompanyDto
    {
      Id = company.Id,
      UserId = company.UserId,
      Name = company.Name,
      Email = company.Email,
      PhoneNumber = company.PhoneNumber,
      Address = company.Address,
    };
  }
}


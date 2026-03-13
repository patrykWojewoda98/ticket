using System;
using Application.Dtos;
using MediatR;

namespace Application.Commands.CompanyCommands.DeleteCompany;

public record DeleteCompanyCommand(
  int CompanyId
) : IRequest<CompanyDto>;

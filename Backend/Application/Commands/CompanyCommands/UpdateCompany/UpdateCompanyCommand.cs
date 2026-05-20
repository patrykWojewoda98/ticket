using System;
using Application.Dtos;
using MediatR;

namespace Application.Commands.CompanyCommands.UpdateCompany;

public record UpdateCompanyCommand(
  int CompanyId,
  string? Name,
  string? Email,
  string? PhoneNumber,
  string? Address
) : IRequest<CompanyDto>;

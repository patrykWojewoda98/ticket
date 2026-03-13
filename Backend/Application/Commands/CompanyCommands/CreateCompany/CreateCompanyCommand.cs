using System;
using Application.Dtos;
using MediatR;

namespace Application.Commands.CompanyCommands.CreateCompany;

public record CreateCompanyCommand(
  int UserId,
  string Name,
  string Email,
  string PhoneNumber,
  string Address
) : IRequest<CompanyDto>;

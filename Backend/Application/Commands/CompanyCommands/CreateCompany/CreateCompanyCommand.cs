using System;
using Domain.Entities;
using MediatR;

namespace Application.Commands.CompanyCommands.CreateCompany;

public record CreateCompanyCommand(Company Company) : IRequest<Unit>;

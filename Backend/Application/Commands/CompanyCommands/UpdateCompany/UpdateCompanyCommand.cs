using System;
using Domain.Entities;
using MediatR;

namespace Application.Commands.CompanyCommands.UpdateCompany;

public record UpdateCompanyCommand(Company Company) : IRequest<Unit>;

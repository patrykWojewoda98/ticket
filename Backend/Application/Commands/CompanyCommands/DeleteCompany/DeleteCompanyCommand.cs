using System;
using Domain.Entities;
using MediatR;

namespace Application.Commands.CompanyCommands.DeleteCompany;


public record DeleteCompanyCommand(Company Company) : IRequest<Unit>;

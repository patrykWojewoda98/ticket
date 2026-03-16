using System;
using FluentValidation;

namespace Application.Commands.CompanyCommands.UpdateCompany;

public class UpdateCompanyCommandValidator : AbstractValidator<UpdateCompanyCommand>
{
  public UpdateCompanyCommandValidator()
  {
    RuleFor(command => command.CompanyId)
        .NotEmpty()
        .WithMessage("Company identifier is required.")
        .GreaterThan(0)
        .WithMessage("Company identifier must be a positive integer.");

    RuleFor(command => command.Name)
        .NotEmpty()
        .When(command => command.Name != null)
        .WithMessage("Company name cannot be empty.")
        .MaximumLength(200)
        .WithMessage("Company name cannot exceed 200 characters.");

    RuleFor(command => command.Email)
        .EmailAddress()
        .When(command => !string.IsNullOrEmpty(command.Email))
        .WithMessage("A valid email address is required.");

    RuleFor(command => command.PhoneNumber)
        .Matches(@"^\+?[1-9]\d{1,14}$")
        .When(command => !string.IsNullOrEmpty(command.PhoneNumber))
        .WithMessage("Invalid phone number format.");

    RuleFor(command => command.Address)
        .MaximumLength(500)
        .When(command => command.Address != null)
        .WithMessage("Address cannot exceed 500 characters.");
  }
}

using System;
using FluentValidation;

namespace Application.Commands.CompanyCommands.CreateCompany;

public class CreateCompanyCommandValidator : AbstractValidator<CreateCompanyCommand>
{
  public CreateCompanyCommandValidator()
  {
    RuleFor(command => command.UserId)
        .NotEmpty()
        .WithMessage("User identifier is required.")
        .GreaterThan(0)
        .WithMessage("User identifier must be a positive integer.");

    RuleFor(command => command.Name)
        .NotEmpty()
        .WithMessage("Company name is required.")
        .MaximumLength(200)
        .WithMessage("Company name cannot exceed 200 characters.");

    RuleFor(command => command.Email)
        .NotEmpty()
        .WithMessage("Email address is required.")
        .EmailAddress()
        .WithMessage("A valid email address is required.");

    RuleFor(command => command.PhoneNumber)
        .NotEmpty()
        .WithMessage("Phone number is required.")
        .Matches(@"^\+?[1-9]\d{1,14}$")
        .WithMessage("Invalid phone number format.");

    RuleFor(command => command.Address)
        .NotEmpty()
        .WithMessage("Address is required.")
        .MaximumLength(500)
        .WithMessage("Address cannot exceed 500 characters.");
  }
}

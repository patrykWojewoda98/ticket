using System;
using FluentValidation;

namespace Application.Commands.UserCommands.CreateUser;

public class CreateUserCommandValidator : AbstractValidator<CreateUserCommand>
{
  public CreateUserCommandValidator()
  {
    RuleFor(command => command.Email)
        .NotEmpty()
        .WithMessage("Email address is required.")
        .EmailAddress()
        .WithMessage("A valid email address is required.");

    RuleFor(command => command.Role)
        .NotEmpty()
        .WithMessage("User role is required.")
        .Must(role => role == "Admin" || role == "User")
        .WithMessage("Role must be one of the following: Admin or User.");

    RuleFor(command => command.Name)
        .NotEmpty()
        .WithMessage("User name is required.")
        .MaximumLength(150)
        .WithMessage("Name cannot exceed 150 characters.");

    RuleFor(command => command.Image)
        .MaximumLength(2048)
        .When(command => !string.IsNullOrEmpty(command.Image))
        .WithMessage("Image URL/path is too long.");

    RuleFor(command => command.CompanyId)
        .GreaterThan(0)
        .When(command => command.CompanyId.HasValue)
        .WithMessage("Company identifier must be a positive integer.");
  }
}

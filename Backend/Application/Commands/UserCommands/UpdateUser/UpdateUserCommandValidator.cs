using System;
using FluentValidation;

namespace Application.Commands.UserCommands.UpdateUser;

public class UpdateUserCommandValidator : AbstractValidator<UpdateUserCommand>
{
  public UpdateUserCommandValidator()
  {
    RuleFor(command => command.UserId)
        .NotEmpty()
        .WithMessage("User identifier is required.")
        .GreaterThan(0)
        .WithMessage("User identifier must be a positive integer.");

    RuleFor(command => command.Email)
        .EmailAddress()
        .When(command => !string.IsNullOrEmpty(command.Email))
        .WithMessage("A valid email address is required.");

    RuleFor(command => command.Role)
        .Must(role => role == "Admin" || role == "User")
        .When(command => !string.IsNullOrEmpty(command.Role))
        .WithMessage("Role must be one of the following: Admin or User.");

    RuleFor(command => command.Name)
        .NotEmpty()
        .When(command => command.Name != null)
        .WithMessage("Name cannot be empty.")
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

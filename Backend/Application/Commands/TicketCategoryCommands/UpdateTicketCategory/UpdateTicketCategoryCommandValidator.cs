using System;
using FluentValidation;

namespace Application.Commands.TicketCategoryCommands.UpdateTicketCategory;

public class UpdateTicketCategoryCommandValidator : AbstractValidator<UpdateTicketCategoryCommand>
{
  public UpdateTicketCategoryCommandValidator()
  {
    RuleFor(command => command.TicketCategoryId)
        .NotEmpty()
        .WithMessage("Category identifier is required.")
        .GreaterThan(0)
        .WithMessage("Category identifier must be a positive integer.");

    RuleFor(command => command.Name)
        .NotEmpty()
        .When(command => command.Name != null)
        .WithMessage("Category name cannot be empty.")
        .MinimumLength(3)
        .When(command => command.Name != null)
        .WithMessage("Category name must be at least 3 characters long.")
        .MaximumLength(100)
        .When(command => command.Name != null)
        .WithMessage("Category name cannot exceed 100 characters.");
  }
}

using System;
using FluentValidation;

namespace Application.Commands.TicketCategoryCommands.CreateTicketCategory;

public class CreateTicketCategoryCommandValidator : AbstractValidator<CreateTicketCategoryCommand>
{
  public CreateTicketCategoryCommandValidator()
  {
    RuleFor(command => command.Name)
        .NotEmpty()
        .WithMessage("Category name is required.")
        .MinimumLength(3)
        .WithMessage("Category name must be at least 3 characters long.")
        .MaximumLength(100)
        .WithMessage("Category name cannot exceed 100 characters.");
  }
}

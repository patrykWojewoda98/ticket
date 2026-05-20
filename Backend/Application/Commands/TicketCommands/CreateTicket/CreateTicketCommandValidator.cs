using System;
using FluentValidation;

namespace Application.Commands.TicketCommands.CreateTicket;

public class CreateTicketCommandValidator : AbstractValidator<CreateTicketCommand>
{
  public CreateTicketCommandValidator()
  {
    RuleFor(command => command.UserId)
        .NotEmpty()
        .WithMessage("Reporter identifier is required.")
        .GreaterThan(0)
        .WithMessage("Reporter identifier must be a positive integer.");

    RuleFor(command => command.AssigneeId)
        .GreaterThan(0)
        .When(command => command.AssigneeId.HasValue)
        .WithMessage("Assignee identifier must be a positive integer.");

    RuleFor(command => command.CategoryId)
        .GreaterThan(0)
        .When(command => command.CategoryId.HasValue)
        .WithMessage("Category identifier must be a positive integer.");

    RuleFor(command => command.StatusId)
        .NotEmpty()
        .WithMessage("Status identifier is required.")
        .GreaterThan(0)
        .WithMessage("Status identifier must be a positive integer.");

    RuleFor(command => command.PriorityId)
        .NotEmpty()
        .WithMessage("Priority identifier is required.")
        .GreaterThan(0)
        .WithMessage("Priority identifier must be a positive integer.");

    RuleFor(command => command.Title)
        .NotEmpty()
        .WithMessage("Ticket title is required.")
        .MinimumLength(5)
        .WithMessage("Title must be at least 5 characters long.")
        .MaximumLength(150)
        .WithMessage("Title cannot exceed 150 characters.");

    RuleFor(command => command.Description)
        .NotEmpty()
        .WithMessage("Ticket description is required.")
        .MinimumLength(10)
        .WithMessage("Description must be at least 10 characters long.")
        .MaximumLength(5000)
        .WithMessage("Description cannot exceed 5000 characters.");
  }
}

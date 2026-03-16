using System;
using FluentValidation;

namespace Application.Commands.TicketCommands.UpdateTicket;

public class UpdateTicketCommandValidator : AbstractValidator<UpdateTicketCommand>
{
  public UpdateTicketCommandValidator()
  {
    RuleFor(command => command.TicketId)
        .NotEmpty()
        .WithMessage("Ticket identifier is required.")
        .GreaterThan(0)
        .WithMessage("Ticket identifier must be a positive integer.");

    RuleFor(command => command.AssigneeId)
        .GreaterThan(0)
        .When(command => command.AssigneeId.HasValue)
        .WithMessage("Assignee identifier must be a positive integer.");

    RuleFor(command => command.CategoryId)
        .GreaterThan(0)
        .When(command => command.CategoryId.HasValue)
        .WithMessage("Category identifier must be a positive integer.");

    RuleFor(command => command.StatusId)
        .GreaterThan(0)
        .When(command => command.StatusId.HasValue)
        .WithMessage("Status identifier must be a positive integer.");

    RuleFor(command => command.PriorityId)
        .GreaterThan(0)
        .When(command => command.PriorityId.HasValue)
        .WithMessage("Priority identifier must be a positive integer.");

    RuleFor(command => command.Title)
        .NotEmpty()
        .When(command => command.Title != null)
        .WithMessage("Title cannot be empty.")
        .MinimumLength(5)
        .When(command => command.Title != null)
        .WithMessage("Title must be at least 5 characters long.")
        .MaximumLength(150)
        .When(command => command.Title != null)
        .WithMessage("Title cannot exceed 150 characters.");

    RuleFor(command => command.Description)
        .NotEmpty()
        .When(command => command.Description != null)
        .WithMessage("Description cannot be empty.")
        .MinimumLength(10)
        .When(command => command.Description != null)
        .WithMessage("Description must be at least 10 characters long.")
        .MaximumLength(5000)
        .When(command => command.Description != null)
        .WithMessage("Description cannot exceed 5000 characters.");
  }
}

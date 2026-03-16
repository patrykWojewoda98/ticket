using System;
using FluentValidation;

namespace Application.Commands.TicketNotificationCommands.CreateTicketNotification;

public class CreateTicketNotificationCommandValidator : AbstractValidator<CreateTicketNotificationCommand>
{
  public CreateTicketNotificationCommandValidator()
  {
    RuleFor(command => command.TicketId)
        .NotEmpty()
        .WithMessage("Ticket identifier is required.")
        .GreaterThan(0)
        .WithMessage("Ticket identifier must be a positive integer.");

    RuleFor(command => command.UserId)
        .NotEmpty()
        .WithMessage("User identifier is required.")
        .GreaterThan(0)
        .WithMessage("User identifier must be a positive integer.");

    RuleFor(command => command.Message)
        .NotEmpty()
        .WithMessage("Notification message cannot be empty.")
        .MaximumLength(500)
        .WithMessage("Notification message cannot exceed 500 characters.");
  }
}

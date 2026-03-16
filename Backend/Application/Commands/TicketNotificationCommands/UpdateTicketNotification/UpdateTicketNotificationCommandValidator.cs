using System;
using FluentValidation;

namespace Application.Commands.TicketNotificationCommands.UpdateTicketNotification;

public class UpdateTicketNotificationCommandValidator : AbstractValidator<UpdateTicketNotificationCommand>
{
  public UpdateTicketNotificationCommandValidator()
  {
    RuleFor(command => command.TicketNotificationId)
        .NotEmpty()
        .WithMessage("Notification identifier is required.")
        .GreaterThan(0)
        .WithMessage("Notification identifier must be a positive integer.");

    RuleFor(command => command.TicketId)
        .NotEmpty()
        .GreaterThan(0)
        .WithMessage("Ticket identifier must be a positive integer.");

    RuleFor(command => command.UserId)
        .NotEmpty()
        .GreaterThan(0)
        .WithMessage("User identifier must be a positive integer.");

    RuleFor(command => command.Message)
        .NotEmpty()
        .When(command => command.Message != null)
        .WithMessage("Notification message cannot be empty.")
        .MaximumLength(500)
        .WithMessage("Notification message cannot exceed 500 characters.");
  }
}

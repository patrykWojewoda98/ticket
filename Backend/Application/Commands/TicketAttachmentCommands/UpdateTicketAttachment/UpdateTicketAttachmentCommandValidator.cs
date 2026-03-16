using System;
using FluentValidation;

namespace Application.Commands.TicketAttachmentCommands.UpdateTicketAttachment;

public class UpdateTicketAttachmentCommandValidator : AbstractValidator<UpdateTicketAttachmentCommand>
{
  public UpdateTicketAttachmentCommandValidator()
  {
    RuleFor(command => command.TicketAttachmentId)
        .NotEmpty()
        .WithMessage("Ticket attachment identifier is required.")
        .GreaterThan(0)
        .WithMessage("Ticket attachment identifier must be a positive integer.");

    RuleFor(command => command.Filename)
        .NotEmpty()
        .When(command => command.Filename != null)
        .WithMessage("Filename cannot be empty.")
        .MaximumLength(255)
        .WithMessage("Filename cannot exceed 255 characters.");

    RuleFor(command => command.Path)
        .NotEmpty()
        .When(command => command.Path != null)
        .WithMessage("File path cannot be empty.")
        .MaximumLength(1000)
        .WithMessage("Path cannot exceed 1000 characters.");

    RuleFor(command => command.UploadedBy)
        .MaximumLength(150)
        .When(command => !string.IsNullOrEmpty(command.UploadedBy))
        .WithMessage("Uploader name cannot exceed 150 characters.");
  }
}

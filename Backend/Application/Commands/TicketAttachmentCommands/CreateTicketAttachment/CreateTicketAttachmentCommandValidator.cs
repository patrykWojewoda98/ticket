using System;
using FluentValidation;

namespace Application.Commands.TicketAttachmentCommands.CreateTicketAttachment;

public class CreateTicketAttachmentCommandValidator : AbstractValidator<CreateTicketAttachmentCommand>
{
  public CreateTicketAttachmentCommandValidator()
  {
    RuleFor(command => command.TicketId)
        .NotEmpty()
        .WithMessage("Ticket identifier is required.")
        .GreaterThan(0)
        .WithMessage("Ticket identifier must be a positive integer.");

    RuleFor(command => command.Filename)
        .NotEmpty()
        .WithMessage("Filename is required.")
        .MaximumLength(255)
        .WithMessage("Filename cannot exceed 255 characters.");

    RuleFor(command => command.Path)
        .NotEmpty()
        .WithMessage("File path is required.")
        .MaximumLength(1000)
        .WithMessage("Path cannot exceed 1000 characters.");

    RuleFor(command => command.UploadedBy)
        .MaximumLength(150)
        .When(command => !string.IsNullOrEmpty(command.UploadedBy))
        .WithMessage("Uploader name cannot exceed 150 characters.");
  }
}

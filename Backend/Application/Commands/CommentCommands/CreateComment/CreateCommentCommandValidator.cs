using System;
using FluentValidation;

namespace Application.Commands.CommentCommands.CreateComment;

public class CreateCommentCommandValidator : AbstractValidator<CreateCommentCommand>
{
  public CreateCommentCommandValidator()
  {
    RuleFor(command => command.UserId)
        .NotEmpty()
        .WithMessage("User identifier is required.")
        .GreaterThan(0)
        .WithMessage("User identifier must be a positive integer.");

    RuleFor(command => command.TicketId)
        .NotEmpty()
        .WithMessage("Ticket identifier is required.")
        .GreaterThan(0)
        .WithMessage("Ticket identifier must be a positive integer.");

    RuleFor(command => command.Content)
        .NotEmpty()
        .WithMessage("Comment content cannot be empty.")
        .MaximumLength(2000)
        .WithMessage("Comment content cannot exceed 2000 characters.");
  }
}

using System;
using FluentValidation;

namespace Application.Commands.CommentCommands.UpdateComment;

public class UpdateCommentCommandValidator : AbstractValidator<UpdateCommentCommand>
{
  public UpdateCommentCommandValidator()
  {
    RuleFor(command => command.CommentId)
        .NotEmpty()
        .WithMessage("Comment identifier is required.")
        .GreaterThan(0)
        .WithMessage("Comment identifier must be a positive integer.");

    RuleFor(command => command.Content)
        .NotEmpty()
        .WithMessage("Comment content cannot be empty.")
        .MaximumLength(2000)
        .WithMessage("Comment content cannot exceed 2000 characters.");
  }
}

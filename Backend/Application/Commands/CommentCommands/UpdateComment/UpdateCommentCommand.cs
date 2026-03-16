using System;
using Application.Dtos;
using MediatR;

namespace Application.Commands.CommentCommands.UpdateComment;

public record UpdateCommentCommand(
  int CommentId,
  string? Content
) : IRequest<CommentDto>;

using System;
using Application.Dtos;
using MediatR;

namespace Application.Commands.CommentCommands.DeleteComment;

public record DeleteCommentCommand(
  int CommentId
) : IRequest<CommentDto>;

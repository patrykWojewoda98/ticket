using System;
using Application.Dtos;
using MediatR;

namespace Application.Commands.CommentCommands.CreateComment;

public record UpdateCommentCommand(
  int UserId,
  int TicketId,
  string Content
) : IRequest<CommentDto>;

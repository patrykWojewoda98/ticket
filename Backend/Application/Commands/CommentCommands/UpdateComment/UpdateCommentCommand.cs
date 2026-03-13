using System;
using Domain.Entities;
using MediatR;

namespace Application.Commands.CommentCommands.UpdateComment;

public record UpdateCommentCommand(Comment Comment) : IRequest<Unit>;

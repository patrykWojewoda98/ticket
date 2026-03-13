using System;
using Domain.Entities;
using MediatR;

namespace Application.Commands.CommentCommands.DeleteComment;

public record DeleteCommentCommand(Comment Comment) : IRequest<Unit>;

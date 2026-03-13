using System;
using Domain.Entities;
using MediatR;

namespace Application.Commands.CommentCommands.CreateComment;

public record UpdateCommentCommand(Comment Comment) : IRequest<Unit>;

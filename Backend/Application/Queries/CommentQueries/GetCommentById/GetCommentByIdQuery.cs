using System;
using Application.Dtos;
using MediatR;

namespace Application.Queries.CommentQueries.GetCommentById;

public record GetCommentByIdQuery(int Id) : IRequest<CommentDto>;

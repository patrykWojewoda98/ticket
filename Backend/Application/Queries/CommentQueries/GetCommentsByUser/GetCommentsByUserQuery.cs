using System;
using Application.Dtos;
using MediatR;

namespace Application.Queries.CommentQueries.GetCommentsByUser;

public record GetCommentsByUserQuery(int UserId) : IRequest<List<CommentDto>>;

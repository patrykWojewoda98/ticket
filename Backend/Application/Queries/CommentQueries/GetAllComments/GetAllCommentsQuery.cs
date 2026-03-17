using System;
using Application.Dtos;
using MediatR;

namespace Application.Queries.CommentQueries.GetAllComments;

public record GetAllCommentsQuery() : IRequest<List<CommentDto>>;

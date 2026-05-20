using System;
using Application.Dtos;
using MediatR;

namespace Application.Queries.CommentQueries.FindByTicketId;

public record GetCommentsByTicketQuery(int TicketId) : IRequest<List<CommentDto>>;

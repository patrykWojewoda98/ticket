using System;
using Application.Dtos;
using Domain.Abstractions;
using MediatR;

namespace Application.Queries.CommentQueries.FindByTicketId;

public class GetCommentsByTicketHandler : IRequestHandler<GetCommentsByTicketQuery, List<CommentDto>>
{
  private readonly ICommentRepository _repository;

  public GetCommentsByTicketHandler(ICommentRepository repository)
  {
    _repository = repository;
  }

  public async Task<List<CommentDto>> Handle(GetCommentsByTicketQuery request, CancellationToken ct)
  {
    var comments = await _repository.FindByTicketIdAsync(request.TicketId);
    return comments.Select(comment => new CommentDto
    {
      Id = comment.Id,
      Content = comment.Content,
      TicketId = comment.TicketId,
      UserId = comment.UserId
    }).ToList();
  }
}

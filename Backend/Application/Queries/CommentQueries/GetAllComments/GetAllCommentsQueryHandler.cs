using System;
using Application.Dtos;
using Domain.Abstractions;
using MediatR;

namespace Application.Queries.CommentQueries.GetAllComments;

public class GetAllCommentsQueryHandler : IRequestHandler<GetAllCommentsQuery, List<CommentDto>>
{
  private readonly ICommentRepository _repository;

  public GetAllCommentsQueryHandler(ICommentRepository repository)
  {
    _repository = repository;
  }

  public async Task<List<CommentDto>> Handle(GetAllCommentsQuery request, CancellationToken cancellationToken)
  {
    var comments = await _repository.GetAllAsync();
    return comments.Select(comment => new CommentDto
    {
      Id = comment.Id,
      UserId = comment.UserId,
      TicketId = comment.TicketId,
      Content = comment.Content
    }).ToList();
  }
}

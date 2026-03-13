using System;
using Application.Dtos;
using Domain.Abstractions;
using MediatR;

namespace Application.Queries.CommentQueries.GetCommentsByUser;

public class GetCommentsByUserQueryHandler : IRequestHandler<GetCommentsByUserQuery, List<CommentDto>>
{
  private readonly ICommentRepository _repository;

  public GetCommentsByUserQueryHandler(ICommentRepository repository)
  {
    _repository = repository;
  }

  public async Task<List<CommentDto>> Handle(GetCommentsByUserQuery request, CancellationToken ct)
  {
    var comments = await _repository.FindByUserIdAsync(request.UserId);
    return comments.Select(comment => new CommentDto
    {
      Id = comment.Id,
      Content = comment.Content
    }).ToList();
  }
}

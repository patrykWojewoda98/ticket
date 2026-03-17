using System;
using Application.Dtos;
using Domain.Abstractions;
using MediatR;

namespace Application.Queries.CommentQueries.GetCommentById;

public class GetCommentByIdQueryHandler : IRequestHandler<GetCommentByIdQuery, CommentDto>
{
  private readonly ICommentRepository _repository;

  public GetCommentByIdQueryHandler(ICommentRepository repository)
  {
    _repository = repository;
  }

  public async Task<CommentDto> Handle(GetCommentByIdQuery request, CancellationToken cancellationToken)
  {
    var comment = await _repository.GetByIdAsync(request.Id);
    if (comment == null) return null;

    return new CommentDto
    {
      Id = comment.Id,
      UserId = comment.UserId,
      TicketId = comment.TicketId,
      Content = comment.Content
    };
  }
}

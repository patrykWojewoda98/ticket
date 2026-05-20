using System;
using Application.Dtos;
using Domain.Abstractions;
using MediatR;

namespace Application.Commands.CommentCommands.UpdateComment;

public class UpdateCommentCommandHandler : IRequestHandler<UpdateCommentCommand, CommentDto>
{
  private readonly ICommentRepository _repository;
  private readonly IUnitOfWorkService _unitOfWork;

  public UpdateCommentCommandHandler(ICommentRepository repository, IUnitOfWorkService unitOfWork)
  {
    _repository = repository;
    _unitOfWork = unitOfWork;
  }
  public async Task<CommentDto> Handle(UpdateCommentCommand request, CancellationToken cancellationToken)
  {
    var comment = await _repository.GetByIdAsync(request.CommentId, cancellationToken);
    if (comment == null) return null;

    comment.Content = request.Content;

    _repository.UpdateEntity(comment);
    await _unitOfWork.SaveChangesAsync(cancellationToken);
    return new CommentDto
    {
      Id = comment.Id,
      UserId = comment.UserId,
      TicketId = comment.TicketId,
      Content = comment.Content
    };
  }
}

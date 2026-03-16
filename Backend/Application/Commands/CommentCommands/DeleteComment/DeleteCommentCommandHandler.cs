using System;
using Application.Dtos;
using Domain.Abstractions;
using MediatR;

namespace Application.Commands.CommentCommands.DeleteComment;

public class DeleteCommentCommandHandler : IRequestHandler<DeleteCommentCommand, CommentDto>
{
  private readonly ICommentRepository _repository;
  private readonly IUnitOfWorkService _unitOfWork;

  public DeleteCommentCommandHandler(ICommentRepository repository, IUnitOfWorkService unitOfWork)
  {
    _repository = repository;
    _unitOfWork = unitOfWork;
  }

  public async Task<CommentDto> Handle(DeleteCommentCommand request, CancellationToken cancellationToken)
  {
    var Comment = await _repository.GetByIdAsync(request.CommentId, cancellationToken);
    if (Comment == null) return null;

    var CommentDto = new CommentDto
    {
      Id = Comment.Id,
      UserId = Comment.UserId,
      TicketId = Comment.TicketId,
      Content = Comment.Content
    };

    _repository.DeleteEntity(Comment);
    await _unitOfWork.SaveChangesAsync(cancellationToken);
    return CommentDto;
  }
}

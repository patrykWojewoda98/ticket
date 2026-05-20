using System;
using Application.Dtos;
using Domain.Abstractions;
using Domain.Entities;
using MediatR;

namespace Application.Commands.CommentCommands.CreateComment;

public class CreateCommentCommandHandler : IRequestHandler<CreateCommentCommand, CommentDto>
{
  private readonly ICommentRepository _repository;
  private readonly IUnitOfWorkService _unitOfWork;

  public CreateCommentCommandHandler(ICommentRepository repository, IUnitOfWorkService unitOfWork)
  {
    _repository = repository;
    _unitOfWork = unitOfWork;
  }

  public async Task<CommentDto> Handle(CreateCommentCommand request, CancellationToken cancellationToken)
  {
    var comment = new Comment
    {
      UserId = request.UserId,
      TicketId = request.TicketId,
      Content = request.Content
    };

    _repository.CreateEntity(comment);
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

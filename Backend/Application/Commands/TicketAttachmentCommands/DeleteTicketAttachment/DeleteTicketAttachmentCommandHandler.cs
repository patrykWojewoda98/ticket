using System;
using Application.Dtos;
using Domain.Abstractions;
using MediatR;

namespace Application.Commands.TicketAttachmentCommands.DeleteTicketAttachment;

public class DeleteTicketAttachmentCommandHandler : IRequestHandler<DeleteTicketAttachmentCommand, TicketAttachmentDto>
{
  private readonly ITicketAttachmentRepository _repository;
  private readonly IUnitOfWorkService _unitOfWork;

  public DeleteTicketAttachmentCommandHandler(ITicketAttachmentRepository repository, IUnitOfWorkService unitOfWork)
  {
    _repository = repository;
    _unitOfWork = unitOfWork;
  }

  public async Task<TicketAttachmentDto> Handle(DeleteTicketAttachmentCommand request, CancellationToken cancellationToken)
  {
    var ticketAttachment = await _repository.GetByIdAsync(request.TicketAttachmentId, cancellationToken);
    if (ticketAttachment == null) return null;

    var TicketAttachmentDto = new TicketAttachmentDto
    {
      Id = ticketAttachment.Id,
      TicketId = ticketAttachment.TicketId,
      Filename = ticketAttachment.Filename,
      Path = ticketAttachment.Path,
      UploadedBy = ticketAttachment.UploadedBy
    };

    _repository.DeleteEntity(ticketAttachment);
    await _unitOfWork.SaveChangesAsync(cancellationToken);
    return TicketAttachmentDto;
  }
}

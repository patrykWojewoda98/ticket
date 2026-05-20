using System;
using Application.Dtos;
using Domain.Abstractions;
using MediatR;

namespace Application.Commands.TicketAttachmentCommands.UpdateTicketAttachment;

public class UpdateTicketAttachmentCommandHandler : IRequestHandler<UpdateTicketAttachmentCommand, TicketAttachmentDto>
{
  private readonly ITicketAttachmentRepository _repository;
  private readonly IUnitOfWorkService _unitOfWork;

  public UpdateTicketAttachmentCommandHandler(ITicketAttachmentRepository repository, IUnitOfWorkService unitOfWork)
  {
    _repository = repository;
    _unitOfWork = unitOfWork;
  }
  public async Task<TicketAttachmentDto> Handle(UpdateTicketAttachmentCommand request, CancellationToken cancellationToken)
  {
    var ticketAttachment = await _repository.GetByIdAsync(request.TicketAttachmentId, cancellationToken);
    if (ticketAttachment == null) return null;

    ticketAttachment.Filename = request.Filename;
    ticketAttachment.Path = request.Path;
    ticketAttachment.UploadedBy = request.UploadedBy;

    _repository.UpdateEntity(ticketAttachment);
    await _unitOfWork.SaveChangesAsync(cancellationToken);
    return new TicketAttachmentDto
    {
      Id = ticketAttachment.Id,
      TicketId = ticketAttachment.TicketId,
      Filename = ticketAttachment.Filename,
      Path = ticketAttachment.Path,
      UploadedBy = ticketAttachment.UploadedBy
    };
  }
}

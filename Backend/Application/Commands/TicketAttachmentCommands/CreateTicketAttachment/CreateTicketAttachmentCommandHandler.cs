using System;
using Application.Dtos;
using Domain.Abstractions;
using Domain.Entities;
using MediatR;

namespace Application.Commands.TicketAttachmentCommands.CreateTicketAttachment;

public class CreateTicketAttachmentCommandHandler : IRequestHandler<CreateTicketAttachmentCommand, TicketAttachmentDto>
{
  private readonly ITicketAttachmentRepository _repository;
  private readonly IUnitOfWorkService _unitOfWork;

  public CreateTicketAttachmentCommandHandler(ITicketAttachmentRepository repository, IUnitOfWorkService unitOfWork)
  {
    _repository = repository;
    _unitOfWork = unitOfWork;
  }

  public async Task<TicketAttachmentDto> Handle(CreateTicketAttachmentCommand request, CancellationToken cancellationToken)
  {
    var ticketAttachment = new TicketAttachment
    {
      TicketId = request.TicketId,
      Filename = request.Filename,
      Path = request.Path,
      UploadedBy = request.UploadedBy
    };

    _repository.CreateEntity(ticketAttachment);
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


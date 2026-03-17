using System;
using Application.Dtos;
using Domain.Abstractions;
using MediatR;

namespace Application.Queries.TicketAttachmentQueries.GetTicketAttachmentById;

public class GetTicketAttachmentByIdQueryHandler : IRequestHandler<GetTicketAttachmentByIdQuery, TicketAttachmentDto>
{
  private readonly ITicketAttachmentRepository _repository;

  public GetTicketAttachmentByIdQueryHandler(ITicketAttachmentRepository repository)
  {
    _repository = repository;
  }

  public async Task<TicketAttachmentDto> Handle(GetTicketAttachmentByIdQuery request, CancellationToken cancellationToken)
  {
    var ticketAttachment = await _repository.GetByIdAsync(request.Id);
    if (ticketAttachment == null) return null;

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

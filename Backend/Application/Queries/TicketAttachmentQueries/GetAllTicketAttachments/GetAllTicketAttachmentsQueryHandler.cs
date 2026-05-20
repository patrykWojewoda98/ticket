using System;
using Application.Dtos;
using Domain.Abstractions;
using MediatR;

namespace Application.Queries.TicketAttachmentQueries.GetAllTicketAttachments;

public class GetAllTicketAttachmentsQueryHandler : IRequestHandler<GetAllTicketAttachmentsQuery, List<TicketAttachmentDto>>
{
  private readonly ITicketAttachmentRepository _repository;

  public GetAllTicketAttachmentsQueryHandler(ITicketAttachmentRepository repository)
  {
    _repository = repository;
  }

  public async Task<List<TicketAttachmentDto>> Handle(GetAllTicketAttachmentsQuery request, CancellationToken cancellationToken)
  {
    var ticketAttachments = await _repository.GetAllAsync();
    return ticketAttachments.Select(ticketAttachment => new TicketAttachmentDto
    {
      Id = ticketAttachment.Id,
      TicketId = ticketAttachment.TicketId,
      Filename = ticketAttachment.Filename,
      Path = ticketAttachment.Path,
      UploadedBy = ticketAttachment.UploadedBy
    }).ToList();
  }
}

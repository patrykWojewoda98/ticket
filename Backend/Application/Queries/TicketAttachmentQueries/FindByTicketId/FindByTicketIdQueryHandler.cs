using System;
using Application.Dtos;
using Domain.Abstractions;
using MediatR;

namespace Application.Queries.TicketAttachmentQueries.FindByTicketId;

public class FindByTicketIdQueryHandler : IRequestHandler<FindByTicketIdQuery, List<TicketAttachmentDto>>
{
  private readonly ITicketAttachmentRepository _repository;

  public FindByTicketIdQueryHandler(ITicketAttachmentRepository repository)
  {
    _repository = repository;
  }

  public async Task<List<TicketAttachmentDto>> Handle(FindByTicketIdQuery request, CancellationToken cancellationToken)
  {
    var ticketAttachments = await _repository.FindByTicketIdAsync(request.TicketId);
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

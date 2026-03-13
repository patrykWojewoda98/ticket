using System;
using Domain.Entities;

namespace Domain.Abstractions;

public interface ITicketAttachmentRepository : IBaseRepository<TicketAttachment>
{
  Task<List<TicketAttachment>> FindByTicketIdAsync(int ticketId);
  Task<IEnumerable<object>> FindByTicketIdAsync(int ticketId, CancellationToken cancellationToken);
}

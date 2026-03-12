using System;
using Domain.Entities;

namespace Domain.Abstractions;

public interface ITicketAttachmentRepository
{
  Task<IEnumerable<TicketAttachment>> GetAllAsync();
  Task<TicketAttachment?> GetByIdAsync(string id);
  Task<IEnumerable<TicketAttachment>> FindByTicketIdAsync(string ticketId);
  Task<TicketAttachment> CreateAsync(TicketAttachment ticketAttachment);
  Task<TicketAttachment> UpdateAsync(string id, TicketAttachment ticketAttachment);
  Task DeleteAsync(string id);
}

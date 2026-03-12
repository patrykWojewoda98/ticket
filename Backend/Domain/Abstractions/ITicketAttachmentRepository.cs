using System;
using Domain.Entities;

namespace Domain.Abstractions;

public interface ITicketAttachmentRepository
{
  Task<List<TicketAttachment>> GetAllAsync();
  Task<TicketAttachment?> GetByIdAsync(string id);
  Task<List<TicketAttachment>> FindByTicketIdAsync(string ticketId);
  Task<TicketAttachment> CreateAsync(TicketAttachment ticketAttachment);
  Task<TicketAttachment> UpdateAsync(string id, TicketAttachment ticketAttachment);
  Task DeleteAsync(string id);
}

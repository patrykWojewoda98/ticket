using System;
using Domain.Entities;

namespace Domain.Abstractions;

public interface ITicketHistoryRepository
{
  Task<List<TicketHistory>> GetAllAsync();
  Task<TicketHistory?> GetByIdAsync(string id);
  Task<List<TicketHistory>> FindByTicketIdAsync(string ticketId);
  Task<TicketHistory> CreateAsync(TicketHistory ticketHistory);
  Task<TicketHistory> UpdateAsync(string id, TicketHistory ticketHistory);
  Task DeleteAsync(string id);
}

using System;
using Domain.Entities;

namespace Domain.Abstractions;

public interface ITicketStatusRepository
{
  Task<IEnumerable<TicketStatus>> GetAllAsync();
  Task<TicketStatus?> GetByIdAsync(string id);
  Task<TicketStatus?> FindByNameAsync(string name);
  Task<TicketStatus> CreateAsync(TicketStatus ticketStatus);
  Task<TicketStatus> UpdateAsync(string id, TicketStatus ticketStatus);
  Task DeleteAsync(string id);
}

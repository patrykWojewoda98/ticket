using System;
using Domain.Entities;

namespace Domain.Abstractions;

public interface ITicketPriorityRepository
{
  Task<IEnumerable<TicketPriority>> GetAllAsync();
  Task<TicketPriority?> GetByIdAsync(string id);
  Task<TicketPriority?> FindByNameAsync(string name);
  Task<TicketPriority> CreateAsync(TicketPriority ticketPriority);
  Task<TicketPriority> UpdateAsync(string id, TicketPriority ticketPriority);
  Task DeleteAsync(string id);
}

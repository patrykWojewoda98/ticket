using System;
using Domain.Entities;

namespace Domain.Abstractions;

public interface ITicketRepository
{
  Task<IEnumerable<Ticket>> GetAllAsync();
  Task<Ticket?> GetByIdAsync(string id);
  Task<IEnumerable<Ticket>> FindByUserIdAsync(string userId);
  Task<IEnumerable<Ticket>> FindByAssigneeIdAsync(string assigneeId);
  Task<Ticket> CreateAsync(Ticket ticket);
  Task<Ticket> UpdateAsync(string id, Ticket ticket);
  Task DeleteAsync(string id);
}

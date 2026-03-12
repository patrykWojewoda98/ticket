using System;
using Domain.Entities;

namespace Domain.Abstractions;

public interface ITicketRepository
{
  Task<List<Ticket>> GetAllAsync();
  Task<Ticket?> GetByIdAsync(string id);
  Task<List<Ticket>> FindByUserIdAsync(string userId);
  Task<List<Ticket>> FindByAssigneeIdAsync(string assigneeId);
  Task<Ticket> CreateAsync(Ticket ticket);
  Task<Ticket> UpdateAsync(string id, Ticket ticket);
  Task DeleteAsync(string id);
}

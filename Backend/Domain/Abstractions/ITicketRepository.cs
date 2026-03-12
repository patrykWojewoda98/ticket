using System;
using Domain.Entities;

namespace Domain.Abstractions;

public interface ITicketRepository : IBaseRepository<Ticket>
{
  Task<List<Ticket>> FindByUserIdAsync(string userId);
  Task<List<Ticket>> FindByAssigneeIdAsync(string assigneeId);
}

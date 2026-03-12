using System;
using Domain.Entities;

namespace Domain.Abstractions;

public interface ITicketRepository : IBaseRepository<Ticket>
{
  Task<List<Ticket>> FindByUserIdAsync(int userId);
  Task<List<Ticket>> FindByAssigneeIdAsync(int assigneeId);
}

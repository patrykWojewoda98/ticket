using System;
using Domain.Entities;

namespace Domain.Abstractions;

public interface ITicketRepository : IBaseRepository<Ticket>
{
  Task<List<Ticket>> FindByUserIdAsync(int userId, CancellationToken cancellationToken = default);
  Task<List<Ticket>> FindByAssigneeIdAsync(int assigneeId, CancellationToken cancellationToken = default);
  Task<int> CountByStatusIdAsync(int statusId, CancellationToken cancellationToken = default);
}

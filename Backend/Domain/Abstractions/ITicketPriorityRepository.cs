using System;
using Domain.Entities;

namespace Domain.Abstractions;

public interface ITicketPriorityRepository : IBaseRepository<TicketPriority>
{
  Task<TicketPriority?> FindByNameAsync(string name);
}

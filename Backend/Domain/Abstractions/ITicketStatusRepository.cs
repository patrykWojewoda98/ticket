using System;
using Domain.Entities;

namespace Domain.Abstractions;

public interface ITicketStatusRepository : IBaseRepository<TicketStatus>
{
  Task<TicketStatus?> FindByNameAsync(string name, CancellationToken cancellationToken = default);
}

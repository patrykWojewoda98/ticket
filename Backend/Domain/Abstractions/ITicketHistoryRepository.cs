using System;
using Domain.Entities;

namespace Domain.Abstractions;

public interface ITicketHistoryRepository : IBaseRepository<TicketHistory>
{
  Task<List<TicketHistory>> FindByTicketIdAsync(int ticketId);
}

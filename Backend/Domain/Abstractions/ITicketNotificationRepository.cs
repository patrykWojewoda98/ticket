using System;
using Domain.Entities;

namespace Domain.Abstractions;

public interface ITicketNotificationRepository : IBaseRepository<TicketNotification>
{
  Task<List<TicketNotification>> FindByTicketIdAsync(int ticketId);
}

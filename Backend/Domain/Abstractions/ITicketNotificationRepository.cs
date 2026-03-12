using System;
using Domain.Entities;

namespace Domain.Abstractions;

public interface ITicketNotificationRepository
{
  Task<IEnumerable<TicketNotification>> GetAllAsync();
  Task<TicketNotification?> GetByIdAsync(string id);
  Task<IEnumerable<TicketNotification>> FindByTicketIdAsync(string ticketId);
  Task<TicketNotification> CreateAsync(TicketNotification ticketNotification);
  Task<TicketNotification> UpdateAsync(string id, TicketNotification ticketNotification);
  Task DeleteAsync(string id);
}

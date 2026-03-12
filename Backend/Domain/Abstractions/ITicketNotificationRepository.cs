using System;
using Domain.Entities;

namespace Domain.Abstractions;

public interface ITicketNotificationRepository
{
  Task<List<TicketNotification>> GetAllAsync();
  Task<TicketNotification?> GetByIdAsync(string id);
  Task<List<TicketNotification>> FindByTicketIdAsync(string ticketId);
  Task<TicketNotification> CreateAsync(TicketNotification ticketNotification);
  Task<TicketNotification> UpdateAsync(string id, TicketNotification ticketNotification);
  Task DeleteAsync(string id);
}

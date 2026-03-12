using System;

namespace Domain.Entities;

public class TicketHistory
{
  public string Id { get; set; }
  public string TicketId { get; set; }
  public string Action { get; set; }
  public string? OldValue { get; set; }
  public string? NewValue { get; set; }
  public string? UserId { get; set; }
  public DateTime CreatedAt { get; set; }

  public Ticket Ticket { get; set; }
  public User? User { get; set; }
}

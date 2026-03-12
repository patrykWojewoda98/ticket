using System;

namespace Domain.Entities;

public class TicketHistory : Base
{
  public int TicketId { get; set; }
  public int UserId { get; set; }
  public string Action { get; set; }
  public string? OldValue { get; set; }
  public string? NewValue { get; set; }

  public Ticket Ticket { get; set; }
  public User? User { get; set; }
}

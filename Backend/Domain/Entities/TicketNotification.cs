using System;

namespace Domain.Entities;

public class TicketNotification
{
  public string Id { get; set; }
  public string TicketId { get; set; }
  public string UserId { get; set; }
  public string Message { get; set; }
  public bool Read { get; set; }
  public DateTime CreatedAt { get; set; }

  public Ticket Ticket { get; set; }
  public User User { get; set; }
}

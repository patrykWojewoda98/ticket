using System;

namespace Domain.Entities;

public class TicketNotification : Base
{
  public int TicketId { get; set; }
  public int UserId { get; set; }
  public string Message { get; set; }
  public bool Read { get; set; }

  public Ticket Ticket { get; set; }
  public User User { get; set; }
}

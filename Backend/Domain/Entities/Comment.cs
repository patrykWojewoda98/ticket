using System;

namespace Domain.Entities;

public class Comment : Base
{
  public string UserId { get; set; }
  public string TicketId { get; set; }
  public string Content { get; set; }

  public User User { get; set; }
  public Ticket Ticket { get; set; }
}

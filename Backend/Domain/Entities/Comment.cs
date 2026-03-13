using System;

namespace Domain.Entities;

public class Comment : Base
{
  public int UserId { get; set; }
  public int TicketId { get; set; }
  public string Content { get; set; }

  public User User { get; set; }
  public Ticket Ticket { get; set; }
}

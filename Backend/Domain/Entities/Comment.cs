using System;

namespace Domain.Entities;

public class Comment
{
  public string Id { get; set; }
  public string UserId { get; set; }
  public string TicketId { get; set; }
  public string Content { get; set; }
  public DateTime CreatedAt { get; set; }
  public DateTime UpdatedAt { get; set; }

  public User User { get; set; }
  public Ticket Ticket { get; set; }
}

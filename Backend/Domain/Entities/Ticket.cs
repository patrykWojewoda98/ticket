using System;

namespace Domain.Entities;

public class Ticket
{
  public string Id { get; set; }
  public string UserId { get; set; }
  public string? AssigneeId { get; set; }
  public string? CategoryId { get; set; }
  public string StatusId { get; set; }
  public string PriorityId { get; set; }
  public string Title { get; set; }
  public string Description { get; set; }
  public DateTime CreatedAt { get; set; }
  public DateTime UpdatedAt { get; set; }

  public User User { get; set; }
  public User? Assignee { get; set; }
  public TicketStatus Status { get; set; }
  public TicketPriority Priority { get; set; }
  public TicketCategory? Category { get; set; }
  public List<TicketAttachment> Attachments { get; set; }
  public List<TicketHistory> History { get; set; }
  public List<TicketNotification> Notifications { get; set; }
  public List<Comment> Comments { get; set; }
}

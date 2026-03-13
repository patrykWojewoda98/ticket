using System;

namespace Domain.Entities;

public class Ticket : Base
{
  public int UserId { get; set; }
  public int? AssigneeId { get; set; }
  public int? CategoryId { get; set; }
  public int StatusId { get; set; }
  public int PriorityId { get; set; }
  public string Title { get; set; }
  public string Description { get; set; }

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

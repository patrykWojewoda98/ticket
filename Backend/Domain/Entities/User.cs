using System;

namespace Domain.Entities;

public class User : Base
{
  public int? CompanyId { get; set; }
  public string Email { get; set; }
  public string? Password { get; set; }
  public string Role { get; set; }
  public string Name { get; set; }

  public List<Company> Companies { get; set; }
  public List<Ticket> Tickets { get; set; }
  public List<Ticket> Assigned { get; set; }
  public List<TicketHistory> TicketHistories { get; set; }
  public List<TicketAttachment> TicketAttachments { get; set; }
  public List<TicketNotification> TicketNotifications { get; set; }
  public List<Comment> Comments { get; set; }
}

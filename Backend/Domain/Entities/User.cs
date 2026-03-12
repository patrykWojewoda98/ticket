using System;

namespace Domain.Entities;

public class User : Base
{
  public string? CompanyId { get; set; }
  public string Email { get; set; }
  public bool EmailVerified { get; set; }
  public string Role { get; set; }
  public string Name { get; set; }
  public string? Image { get; set; }

  public List<Account> Accounts { get; set; }
  public List<Session> Sessions { get; set; }
  public List<Company> Companies { get; set; }
  public List<Ticket> Tickets { get; set; }
  public List<Ticket> Assigned { get; set; }
  public List<TicketHistory> TicketHistories { get; set; }
  public List<TicketAttachment> TicketAttachments { get; set; }
  public List<TicketNotification> TicketNotifications { get; set; }
  public List<Comment> Comments { get; set; }
}

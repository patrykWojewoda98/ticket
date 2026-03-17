using System;

namespace Domain.Entities;

public class TicketAttachment : Base
{
  public int TicketId { get; set; }
  public string Filename { get; set; }
  public string Path { get; set; }
  public int? UploadedBy { get; set; }

  public Ticket Ticket { get; set; }
  public User? User { get; set; }
}

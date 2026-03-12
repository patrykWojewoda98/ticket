using System;

namespace Domain.Entities;

public class TicketAttachment
{
  public string Id { get; set; }
  public string TicketId { get; set; }
  public string Filename { get; set; }
  public string Path { get; set; }
  public string? UploadedBy { get; set; }
  public DateTime CreatedAt { get; set; }

  public Ticket Ticket { get; set; }
  public User? User { get; set; }
}

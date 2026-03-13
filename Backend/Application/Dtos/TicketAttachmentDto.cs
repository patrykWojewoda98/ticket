using System;

namespace Application.Dtos;

public class TicketAttachmentDto
{
  public int Id { get; set; }
  public int TicketId { get; set; }
  public string Filename { get; set; }
  public string Path { get; set; }
  public string? UploadedBy { get; set; }
}

using System;

namespace Application.Dtos;

public class TicketHistoryDto
{
  public int Id { get; set; }
  public int TicketId { get; set; }
  public int UserId { get; set; }
  public string Action { get; set; }
  public string? OldValue { get; set; }
  public string? NewValue { get; set; }
}

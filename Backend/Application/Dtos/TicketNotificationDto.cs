using System;

namespace Application.Dtos;

public class TicketNotificationDto : BaseDto
{
  public int TicketId { get; set; }
  public int UserId { get; set; }
  public string Message { get; set; }
  public bool Read { get; set; }
}

using System;

namespace Application.Dtos;

public class CommentDto : BaseDto
{
  public int UserId { get; set; }
  public int TicketId { get; set; }
  public string Content { get; set; }
}

using System;

namespace Application.Dtos;

public class TicketDto : BaseDto
{
  public int UserId { get; set; }
  public int? AssigneeId { get; set; }
  public int? CategoryId { get; set; }
  public int StatusId { get; set; }
  public int PriorityId { get; set; }
  public string Title { get; set; }
  public string Description { get; set; }
}

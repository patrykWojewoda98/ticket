using System;

namespace Application.Dtos;

public class SessionDto : BaseDto
{
  public int UserId { get; set; }
  public string Token { get; set; }
  public DateTime ExpiresAt { get; set; }
}

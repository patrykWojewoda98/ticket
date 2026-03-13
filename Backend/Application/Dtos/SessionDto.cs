using System;

namespace Application.Dtos;

public class SessionDto
{
  public int Id { get; set; }
  public int UserId { get; set; }
  public string Token { get; set; }
  public DateTime ExpiresAt { get; set; }
}

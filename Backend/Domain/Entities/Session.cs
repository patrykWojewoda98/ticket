using System;

namespace Domain.Entities;

public class Session
{
  public string Id { get; set; }
  public string UserId { get; set; }
  public string Token { get; set; }
  public DateTime ExpiresAt { get; set; }
  public string? IpAddress { get; set; }
  public string? UserAgent { get; set; }
  public DateTime CreatedAt { get; set; }
  public DateTime UpdatedAt { get; set; }

  public User User { get; set; }
}

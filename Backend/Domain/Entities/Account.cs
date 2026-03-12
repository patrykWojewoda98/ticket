using System;

namespace Domain.Entities;

public class Account : Base
{
  public int UserId { get; set; }
  public int ProviderId { get; set; }
  public int AccountId { get; set; }
  public string? Password { get; set; }
  public string? Scope { get; set; }
  public string? IdToken { get; set; }
  public string? AccessToken { get; set; }
  public string? RefreshToken { get; set; }
  public DateTime? AccessTokenExpiresAt { get; set; }
  public DateTime? RefreshTokenExpiresAt { get; set; }

  public User User { get; set; }
}

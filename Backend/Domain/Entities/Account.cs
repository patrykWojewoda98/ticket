using System;

namespace Domain.Entities;

public class Account
{
  public string Id { get; set; }
  public string UserId { get; set; }
  public string ProviderId { get; set; }
  public string AccountId { get; set; }
  public string? Password { get; set; }
  public string? Scope { get; set; }
  public string? IdToken { get; set; }
  public string? AccessToken { get; set; }
  public string? RefreshToken { get; set; }
  public DateTime? AccessTokenExpiresAt { get; set; }
  public DateTime? RefreshTokenExpiresAt { get; set; }
  public DateTime CreatedAt { get; set; }
  public DateTime UpdatedAt { get; set; }

  public User User { get; set; }
}

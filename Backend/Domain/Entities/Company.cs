using System;

namespace Domain.Entities;

public class Company
{
  public string Id { get; set; }
  public string UserId { get; set; }
  public string Name { get; set; }
  public string Email { get; set; }
  public string PhoneNumber { get; set; }
  public string Address { get; set; }
  public DateTime CreatedAt { get; set; }
  public DateTime UpdatedAt { get; set; }

  public User User { get; set; }
}

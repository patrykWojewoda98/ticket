using System;

namespace Domain.Entities;

public class Company : Base
{
  public string UserId { get; set; }
  public string Name { get; set; }
  public string Email { get; set; }
  public string PhoneNumber { get; set; }
  public string Address { get; set; }

  public User User { get; set; }
}

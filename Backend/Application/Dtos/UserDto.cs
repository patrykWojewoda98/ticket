using System;

namespace Application.Dtos;

public class UserDto : BaseDto
{
  public int? CompanyId { get; set; }
  public string Email { get; set; }
  public string Role { get; set; }
  public string Name { get; set; }
  public DateTime? LockoutEnd { get; set; }
  public bool IsLocked => LockoutEnd.HasValue && LockoutEnd.Value > DateTime.UtcNow;
}

using System;

namespace Application.Dtos;

public class UserDto : BaseDto
{
  public int? CompanyId { get; set; }
  public string Email { get; set; }
  public bool EmailVerified { get; set; }
  public string Role { get; set; }
  public string Name { get; set; }
  public string? Image { get; set; }
}

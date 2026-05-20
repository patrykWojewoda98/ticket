using System;

namespace Application.Dtos;

public class LoginResultDto
{
  public bool IsBlocked { get; set; }
  public UserDto? User { get; set; }
  public string? Message { get; set; }
}

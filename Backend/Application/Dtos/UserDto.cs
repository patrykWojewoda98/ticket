using System;

namespace Application.Dtos;

public class UserDto : BaseDto
{
  public int? CompanyId { get; set; }
  public string Email { get; set; }
  public string Role { get; set; }
  public string Name { get; set; }
<<<<<<< HEAD
  public bool IsBlocked { get; set; }
  public int FailedLoginAttempts { get; set; }
=======
>>>>>>> 8bdda2c58a129a22e9d27085a8ac580aa62d740e
}

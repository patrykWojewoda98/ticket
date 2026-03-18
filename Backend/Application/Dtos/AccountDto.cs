using System;

namespace Application.Dtos;

public class AccountDto : BaseDto
{
  public int UserId { get; set; }
  public string ProviderId { get; set; }
  public int AccountId { get; set; }
  public string? Password { get; set; }
}

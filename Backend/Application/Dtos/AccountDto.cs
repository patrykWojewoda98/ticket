using System;

namespace Application.Dtos;

public class AccountDto : BaseDto
{
  public int UserId { get; set; }
  public int ProviderId { get; set; }
  public int AccountId { get; set; }
}

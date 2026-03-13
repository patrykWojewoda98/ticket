using System;

namespace Application.Dtos;

public class AccountDto
{
  public int Id { get; set; }            // przy Queries będzie zwracany Id
  public int UserId { get; set; }
  public int ProviderId { get; set; }
  public int AccountId { get; set; }
  public string? AccountName { get; set; }
}

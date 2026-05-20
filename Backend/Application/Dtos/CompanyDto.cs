using System;

namespace Application.Dtos;

public class CompanyDto : BaseDto
{
  public int UserId { get; set; }
  public string Name { get; set; }
  public string Email { get; set; }
  public string PhoneNumber { get; set; }
  public string Address { get; set; }
}

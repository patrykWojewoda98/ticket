using System;

namespace Domain.Entities;

public abstract class Base
{
  public int Id { get; set; }
  public DateTime CreatedAt { get; set; }
  public DateTime? UpdatedAt { get; set; }
}

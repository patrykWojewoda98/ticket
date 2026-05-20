using System;

namespace Domain.Entities;

public class BlockedUser : Base
{
  public int UserId { get; set; }
  public DateTime BlockedAt { get; set; }
  public int Attempts { get; set; }
  public string Reason { get; set; }
  public DateTime? UnblockedAt { get; set; }
  public int? UnblockedByAdminId { get; set; }
}

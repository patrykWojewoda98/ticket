using System;

namespace Domain.Entities;

public class TicketStatus : Base
{
  public string Name { get; set; }

  public List<Ticket> Tickets { get; set; }
}

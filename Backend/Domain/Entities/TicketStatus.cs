using System;

namespace Domain.Entities;

public class TicketStatus
{
  public string Id { get; set; }
  public string Name { get; set; }

  public List<Ticket> Tickets { get; set; }
}

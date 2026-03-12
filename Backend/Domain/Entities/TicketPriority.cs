using System;

namespace Domain.Entities;

public class TicketPriority
{
  public string Id { get; set; }
  public string Name { get; set; }

  public List<Ticket> Tickets { get; set; }
}

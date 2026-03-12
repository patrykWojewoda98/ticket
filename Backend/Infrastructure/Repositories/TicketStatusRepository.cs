using System;
using Domain.Abstractions;
using Domain.Entities;
using Infrastructure.Context;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories;

public class TicketStatusRepository : ITicketStatusRepository
{
  private readonly DatabaseContext _context;

  public TicketStatusRepository(DatabaseContext context)
  {
    _context = context;
  }

  public async Task<IEnumerable<TicketStatus>> GetAllAsync()
  {
    return await _context.TicketStatuses.ToListAsync();
  }

  public async Task<TicketStatus?> GetByIdAsync(string id)
  {
    return await _context.TicketStatuses.FindAsync(id);
  }

  public async Task<TicketStatus?> FindByNameAsync(string name)
  {
    return await _context.TicketStatuses
                         .FirstOrDefaultAsync(ticketStatus => ticketStatus.Name == name);
  }

  public async Task<TicketStatus> CreateAsync(TicketStatus ticketStatus)
  {
    await _context.TicketStatuses.AddAsync(ticketStatus);
    await _context.SaveChangesAsync();
    return ticketStatus;
  }

  public async Task<TicketStatus> UpdateAsync(string id, TicketStatus ticketStatus)
  {
    var existingTicketStatus = await _context.TicketStatuses
        .FirstOrDefaultAsync(ticketStatus => ticketStatus.Id == id);

    if (existingTicketStatus != null)
    {
      existingTicketStatus.Name = ticketStatus.Name;

      _context.TicketStatuses.Update(existingTicketStatus);
      await _context.SaveChangesAsync();
      return existingTicketStatus;
    }

    return ticketStatus;
  }

  public async Task DeleteAsync(string id)
  {
    var existingTicketStatus = await _context.TicketStatuses
        .FirstOrDefaultAsync(ticketStatus => ticketStatus.Id == id);

    if (existingTicketStatus != null)
    {
      _context.TicketStatuses.Remove(existingTicketStatus);
      await _context.SaveChangesAsync();
    }
  }
}

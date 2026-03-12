using System;
using Domain.Abstractions;
using Domain.Entities;
using Infrastructure.Context;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories;

public class TicketPriorityRepository : ITicketPriorityRepository
{
  private readonly DatabaseContext _context;

  public TicketPriorityRepository(DatabaseContext context)
  {
    _context = context;
  }

  public async Task<IEnumerable<TicketPriority>> GetAllAsync()
  {
    return await _context.TicketPriorities.ToListAsync();
  }

  public async Task<TicketPriority?> GetByIdAsync(string id)
  {
    return await _context.TicketPriorities.FindAsync(id);
  }

  public async Task<TicketPriority?> FindByNameAsync(string name)
  {
    return await _context.TicketPriorities
                         .FirstOrDefaultAsync(ticketPriority => ticketPriority.Name == name);
  }

  public async Task<TicketPriority> CreateAsync(TicketPriority ticketPriority)
  {
    await _context.TicketPriorities.AddAsync(ticketPriority);
    await _context.SaveChangesAsync();
    return ticketPriority;
  }

  public async Task<TicketPriority> UpdateAsync(string id, TicketPriority ticketPriority)
  {
    var existingTicketPriority = await _context.TicketPriorities
        .FirstOrDefaultAsync(ticketPriority => ticketPriority.Id == id);

    if (existingTicketPriority != null)
    {
      existingTicketPriority.Name = ticketPriority.Name;

      _context.TicketPriorities.Update(existingTicketPriority);
      await _context.SaveChangesAsync();
      return existingTicketPriority;
    }

    return ticketPriority;
  }

  public async Task DeleteAsync(string id)
  {
    var existingTicketPriority = await _context.TicketPriorities
        .FirstOrDefaultAsync(ticketPriority => ticketPriority.Id == id);

    if (existingTicketPriority != null)
    {
      _context.TicketPriorities.Remove(existingTicketPriority);
      await _context.SaveChangesAsync();
    }
  }
}

using System;
using Domain.Abstractions;
using Domain.Entities;
using Infrastructure.Context;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories;

public class TicketHistoryRepository : ITicketHistoryRepository
{
  private readonly DatabaseContext _context;

  public TicketHistoryRepository(DatabaseContext context)
  {
    _context = context;
  }

  public async Task<IEnumerable<TicketHistory>> GetAllAsync()
  {
    return await _context.TicketHistories.ToListAsync();
  }

  public async Task<TicketHistory?> GetByIdAsync(string id)
  {
    return await _context.TicketHistories.FindAsync(id);
  }

  public async Task<IEnumerable<TicketHistory>> FindByTicketIdAsync(string ticketId)
  {
    return await _context.TicketHistories
                         .Where(ticketHistory => ticketHistory.TicketId == ticketId)
                         .ToListAsync();
  }

  public async Task<TicketHistory> CreateAsync(TicketHistory ticketHistory)
  {
    await _context.TicketHistories.AddAsync(ticketHistory);
    await _context.SaveChangesAsync();
    return ticketHistory;
  }

  public async Task<TicketHistory> UpdateAsync(string id, TicketHistory ticketHistory)
  {
    var existingTicketHistory = await _context.TicketHistories
        .FirstOrDefaultAsync(ticketHistory => ticketHistory.Id == id);

    if (existingTicketHistory != null)
    {
      existingTicketHistory.TicketId = ticketHistory.TicketId;
      existingTicketHistory.Action = ticketHistory.Action;
      existingTicketHistory.OldValue = ticketHistory.OldValue;
      existingTicketHistory.NewValue = ticketHistory.NewValue;
      existingTicketHistory.UserId = ticketHistory.UserId;

      _context.TicketHistories.Update(existingTicketHistory);
      await _context.SaveChangesAsync();
      return existingTicketHistory;
    }

    return ticketHistory;
  }

  public async Task DeleteAsync(string id)
  {
    var existingTicketHistory = await _context.TicketHistories
        .FirstOrDefaultAsync(ticketHistory => ticketHistory.Id == id);

    if (existingTicketHistory != null)
    {
      _context.TicketHistories.Remove(existingTicketHistory);
      await _context.SaveChangesAsync();
    }
  }
}

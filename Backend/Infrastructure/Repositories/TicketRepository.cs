using System;
using Domain.Abstractions;
using Domain.Entities;
using Infrastructure.Context;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories;

public class TicketRepository : ITicketRepository
{
  private readonly DatabaseContext _context;

  public TicketRepository(DatabaseContext context)
  {
    _context = context;
  }

  public async Task<IEnumerable<Ticket>> GetAllAsync()
  {
    return await _context.Tickets.ToListAsync();
  }

  public async Task<Ticket?> GetByIdAsync(string id)
  {
    return await _context.Tickets.FindAsync(id);
  }

  public async Task<IEnumerable<Ticket>> FindByUserIdAsync(string userId)
  {
    return await _context.Tickets
                         .Where(ticket => ticket.UserId == userId)
                         .ToListAsync();
  }

  public async Task<IEnumerable<Ticket>> FindByAssigneeIdAsync(string assigneeId)
  {
    return await _context.Tickets
                         .Where(ticket => ticket.AssigneeId == assigneeId)
                         .ToListAsync();
  }

  public async Task<Ticket> CreateAsync(Ticket ticket)
  {
    await _context.Tickets.AddAsync(ticket);
    await _context.SaveChangesAsync();
    return ticket;
  }

  public async Task<Ticket> UpdateAsync(string id, Ticket ticket)
  {
    var existingTicket = await _context.Tickets
        .FirstOrDefaultAsync(ticket => ticket.Id == id);

    if (existingTicket != null)
    {
      existingTicket.UserId = ticket.UserId;
      existingTicket.AssigneeId = ticket.AssigneeId;
      existingTicket.CategoryId = ticket.CategoryId;
      existingTicket.StatusId = ticket.StatusId;
      existingTicket.PriorityId = ticket.PriorityId;
      existingTicket.Title = ticket.Title;
      existingTicket.Description = ticket.Description;
      existingTicket.UpdatedAt = ticket.UpdatedAt;

      _context.Tickets.Update(existingTicket);
      await _context.SaveChangesAsync();
      return existingTicket;
    }

    return ticket;
  }

  public async Task DeleteAsync(string id)
  {
    var existingTicket = await _context.Tickets
        .FirstOrDefaultAsync(ticket => ticket.Id == id);

    if (existingTicket != null)
    {
      _context.Tickets.Remove(existingTicket);
      await _context.SaveChangesAsync();
    }
  }
}

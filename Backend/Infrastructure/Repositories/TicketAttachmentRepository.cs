using System;
using Domain.Abstractions;
using Domain.Entities;
using Infrastructure.Context;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories;

public class TicketAttachmentRepository : ITicketAttachmentRepository
{
  private readonly DatabaseContext _context;

  public TicketAttachmentRepository(DatabaseContext context)
  {
    _context = context;
  }

  public async Task<IEnumerable<TicketAttachment>> GetAllAsync()
  {
    return await _context.TicketAttachments.ToListAsync();
  }

  public async Task<TicketAttachment?> GetByIdAsync(string id)
  {
    return await _context.TicketAttachments.FindAsync(id);
  }

  public async Task<IEnumerable<TicketAttachment>> FindByTicketIdAsync(string ticketId)
  {
    return await _context.TicketAttachments
                         .Where(ticketAttachment => ticketAttachment.TicketId == ticketId)
                         .ToListAsync();
  }

  public async Task<TicketAttachment> CreateAsync(TicketAttachment ticketAttachment)
  {
    await _context.TicketAttachments.AddAsync(ticketAttachment);
    await _context.SaveChangesAsync();
    return ticketAttachment;
  }

  public async Task<TicketAttachment> UpdateAsync(string id, TicketAttachment ticketAttachment)
  {
    var existingTicketAttachment = await _context.TicketAttachments
        .FirstOrDefaultAsync(ticketAttachment => ticketAttachment.Id == id);

    if (existingTicketAttachment != null)
    {
      existingTicketAttachment.TicketId = ticketAttachment.TicketId;
      existingTicketAttachment.Filename = ticketAttachment.Filename;
      existingTicketAttachment.Path = ticketAttachment.Path;
      existingTicketAttachment.UploadedBy = ticketAttachment.UploadedBy;

      _context.TicketAttachments.Update(existingTicketAttachment);
      await _context.SaveChangesAsync();
      return existingTicketAttachment;
    }

    return ticketAttachment;
  }

  public async Task DeleteAsync(string id)
  {
    var existingTicketAttachment = await _context.TicketAttachments
        .FirstOrDefaultAsync(ticketAttachment => ticketAttachment.Id == id);

    if (existingTicketAttachment != null)
    {
      _context.TicketAttachments.Remove(existingTicketAttachment);
      await _context.SaveChangesAsync();
    }
  }
}

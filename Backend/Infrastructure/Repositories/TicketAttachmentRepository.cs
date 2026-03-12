using System;
using Domain.Abstractions;
using Domain.Entities;
using Infrastructure.Context;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories;

public class TicketAttachmentRepository : BaseRepository<TicketAttachment>, ITicketAttachmentRepository
{
  public TicketAttachmentRepository(DatabaseContext databaseContext) : base(databaseContext) { }

  public async Task<List<TicketAttachment>> FindByTicketIdAsync(int ticketId)
  {
    return await _dbSet
                 .Where(ticketAttachment => ticketAttachment.TicketId == ticketId)
                 .ToListAsync();
  }
}

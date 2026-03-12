using System;
using Domain.Abstractions;
using Domain.Entities;
using Infrastructure.Context;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories;

public class TicketCategoryRepository : ITicketCategoryRepository
{
  private readonly DatabaseContext _context;

  public TicketCategoryRepository(DatabaseContext context)
  {
    _context = context;
  }

  public async Task<IEnumerable<TicketCategory>> GetAllAsync()
  {
    return await _context.TicketCategories.ToListAsync();
  }

  public async Task<TicketCategory?> GetByIdAsync(string id)
  {
    return await _context.TicketCategories.FindAsync(id);
  }

  public async Task<TicketCategory?> FindByNameAsync(string name)
  {
    return await _context.TicketCategories
                         .FirstOrDefaultAsync(ticketCategory => ticketCategory.Name == name);
  }

  public async Task<TicketCategory> CreateAsync(TicketCategory category)
  {
    await _context.TicketCategories.AddAsync(category);
    await _context.SaveChangesAsync();
    return category;
  }

  public async Task<TicketCategory> UpdateAsync(string id, TicketCategory category)
  {
    var existingCategory = await _context.TicketCategories
        .FirstOrDefaultAsync(ticketCategory => ticketCategory.Id == id);

    if (existingCategory != null)
    {
      existingCategory.Name = category.Name;

      _context.TicketCategories.Update(existingCategory);
      await _context.SaveChangesAsync();
      return existingCategory;
    }

    return category;
  }

  public async Task DeleteAsync(string id)
  {
    var existingCategory = await _context.TicketCategories
        .FirstOrDefaultAsync(ticketCategory => ticketCategory.Id == id);

    if (existingCategory != null)
    {
      _context.TicketCategories.Remove(existingCategory);
      await _context.SaveChangesAsync();
    }
  }
}

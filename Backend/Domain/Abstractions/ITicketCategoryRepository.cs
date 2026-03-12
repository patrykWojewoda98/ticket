using System;
using Domain.Entities;

namespace Domain.Abstractions;

public interface ITicketCategoryRepository
{
  Task<IEnumerable<TicketCategory>> GetAllAsync();
  Task<TicketCategory?> GetByIdAsync(string id);
  Task<TicketCategory?> FindByNameAsync(string name);
  Task<TicketCategory> CreateAsync(TicketCategory category);
  Task<TicketCategory> UpdateAsync(string id, TicketCategory category);
  Task DeleteAsync(string id);
}

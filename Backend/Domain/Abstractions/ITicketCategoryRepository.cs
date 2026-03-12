using System;
using Domain.Entities;

namespace Domain.Abstractions;

public interface ITicketCategoryRepository : IBaseRepository<TicketCategory>
{
  Task<TicketCategory?> FindByNameAsync(string name);
}

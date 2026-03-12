using System;
using Domain.Entities;

namespace Domain.Abstractions;

public interface ICompanyRepository : IBaseRepository<Company>
{
  Task<List<Company>> FindByUserIdAsync(string userId);
}

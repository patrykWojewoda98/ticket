using System;
using Domain.Abstractions;
using Domain.Entities;
using Infrastructure.Context;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories;

public class CompanyRepository : BaseRepository<Company>, ICompanyRepository
{
  public CompanyRepository(DatabaseContext databaseContext) : base(databaseContext) { }

  public async Task<List<Company>> FindByUserIdAsync(string userId)
  {
    return await _dbSet
                 .Where(company => company.UserId == userId)
                 .ToListAsync();
  }
}

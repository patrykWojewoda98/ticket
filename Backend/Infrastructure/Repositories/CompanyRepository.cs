using System;
using Domain.Abstractions;
using Domain.Entities;
using Infrastructure.Context;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories;

public class CompanyRepository : BaseRepository<Company>, ICompanyRepository
{
  public CompanyRepository(DatabaseContext databaseContext) : base(databaseContext) { }

  public async Task<List<Company>> FindByUserIdAsync(int userId, CancellationToken cancellationToken = default)
  {
    return await _dbContext.Set<Company>()
                 .Where(company => company.UserId == userId)
                 .ToListAsync(cancellationToken);
  }
}

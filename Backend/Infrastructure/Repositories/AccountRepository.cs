using System;
using Domain.Abstractions;
using Domain.Entities;
using Infrastructure.Context;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories;

public class AccountRepository : BaseRepository<Account>, IAccountRepository
{
  public AccountRepository(DatabaseContext databaseContext) : base(databaseContext) { }

  public async Task<List<Account>> FindByUserIdAsync(string userId)
  {
    return await _dbSet
                 .Where(account => account.UserId == userId)
                 .ToListAsync();
  }

  public async Task<Account?> FindByProviderIdAsync(string providerId, string accountId)
  {
    return await _dbSet
                 .FirstOrDefaultAsync(account => account.ProviderId == providerId && account.AccountId == accountId);
  }
}

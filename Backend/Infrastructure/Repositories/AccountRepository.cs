using System;
using Domain.Abstractions;
using Domain.Entities;
using Infrastructure.Context;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories;

public class AccountRepository : BaseRepository<Account>, IAccountRepository
{
  public AccountRepository(DatabaseContext databaseContext) : base(databaseContext) { }

  public async Task<List<Account>> FindByUserIdAsync(int userId, CancellationToken cancellationToken = default)
  {
    return await _dbContext.Set<Account>()
                 .Where(account => account.UserId == userId)
                 .ToListAsync(cancellationToken);
  }

  public async Task<Account?> FindByProviderIdAsync(string providerId, int accountId, CancellationToken cancellationToken = default)
  {
    return await _dbContext.Set<Account>()
                 .FirstOrDefaultAsync(account => account.ProviderId == providerId && account.AccountId == accountId, cancellationToken);
  }
}

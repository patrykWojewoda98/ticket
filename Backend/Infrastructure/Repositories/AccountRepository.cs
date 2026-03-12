using System;
using Domain.Abstractions;
using Domain.Entities;
using Infrastructure.Context;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories;

public class AccountRepository : IAccountRepository
{
  private readonly DatabaseContext _context;

  public AccountRepository(DatabaseContext context)
  {
    _context = context;
  }

  public async Task<IEnumerable<Account>> GetAllAsync()
  {
    return await _context.Accounts.ToListAsync();
  }

  public async Task<Account?> GetByIdAsync(string id)
  {
    return await _context.Accounts.FindAsync(id);
  }

  public async Task<IEnumerable<Account>> FindByUserIdAsync(string userId)
  {
    return await _context.Accounts
                         .Where(account => account.UserId == userId)
                         .ToListAsync();
  }

  public async Task<Account?> FindByProviderIdAsync(string providerId, string accountId)
  {
    return await _context.Accounts
                         .FirstOrDefaultAsync(account => account.ProviderId == providerId && account.AccountId == accountId);
  }

  public async Task<Account> CreateAsync(Account account)
  {
    await _context.Accounts.AddAsync(account);
    await _context.SaveChangesAsync();
    return account;
  }

  public async Task<Account> UpdateAsync(string id, Account account)
  {
    var existingAccount = await _context.Accounts.FindAsync(id);
    if (existingAccount != null)
    {
      existingAccount.UserId = account.UserId;
      existingAccount.ProviderId = account.ProviderId;
      existingAccount.AccountId = account.AccountId;
      existingAccount.Password = account.Password;
      existingAccount.Scope = account.Scope;
      existingAccount.IdToken = account.IdToken;
      existingAccount.AccessToken = account.AccessToken;
      existingAccount.RefreshToken = account.RefreshToken;
      existingAccount.AccessTokenExpiresAt = account.AccessTokenExpiresAt;
      existingAccount.RefreshTokenExpiresAt = account.RefreshTokenExpiresAt;
      existingAccount.UpdatedAt = account.UpdatedAt;

      _context.Accounts.Update(existingAccount);
      await _context.SaveChangesAsync();
      return existingAccount;
    }
    return account;
  }

  public async Task DeleteAsync(string id)
  {
    var existingAccount = await _context.Accounts.FindAsync(id);
    if (existingAccount != null)
    {
      _context.Accounts.Remove(existingAccount);
      await _context.SaveChangesAsync();
    }
  }
}


using System;
using Domain.Entities;

namespace Domain.Abstractions;

public interface IAccountRepository
{
  Task<List<Account>> GetAllAsync();
  Task<Account?> GetByIdAsync(string id);
  Task<List<Account>> FindByUserIdAsync(string userId);
  Task<Account?> FindByProviderIdAsync(string providerId, string accountId);
  Task<Account> CreateAsync(Account account);
  Task<Account> UpdateAsync(string id, Account account);
  Task DeleteAsync(string id);
}

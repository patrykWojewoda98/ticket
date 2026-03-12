using System;
using Domain.Entities;

namespace Domain.Abstractions;

public interface IAccountRepository
{
  Task<IEnumerable<Account>> GetAllAsync();
  Task<Account?> GetByIdAsync(string id);
  Task<IEnumerable<Account>> FindByUserIdAsync(string userId);
  Task<Account?> FindByProviderIdAsync(string providerId, string accountId);
  Task<Account> CreateAsync(Account account);
  Task<Account> UpdateAsync(string id, Account account);
  Task DeleteAsync(string id);
}

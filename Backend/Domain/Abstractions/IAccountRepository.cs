using System;
using Domain.Entities;

namespace Domain.Abstractions;

public interface IAccountRepository : IBaseRepository<Account>
{
  Task<List<Account>> FindByUserIdAsync(string userId);
  Task<Account?> FindByProviderIdAsync(string providerId, string accountId);
}

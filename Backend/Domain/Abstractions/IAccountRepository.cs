using System;
using Domain.Entities;

namespace Domain.Abstractions;

public interface IAccountRepository : IBaseRepository<Account>
{
  Task<List<Account>> FindByUserIdAsync(int userId);
  Task<Account?> FindByProviderIdAsync(int providerId, int accountId);
}

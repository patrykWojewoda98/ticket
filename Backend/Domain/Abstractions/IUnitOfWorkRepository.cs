using System;
using Domain.Entities;

namespace Domain.Abstractions;

public interface IUnitOfWorkRepository
{
  Task SaveChangesAsync(CancellationToken cancellationToken = default);
}

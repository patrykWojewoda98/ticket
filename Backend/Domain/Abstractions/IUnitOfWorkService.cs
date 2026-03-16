using System;
using Domain.Entities;

namespace Domain.Abstractions;

public interface IUnitOfWorkService
{
  Task SaveChangesAsync(CancellationToken cancellationToken = default);
}

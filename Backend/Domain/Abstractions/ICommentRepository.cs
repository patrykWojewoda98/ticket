using System;
using Domain.Entities;

namespace Domain.Abstractions;

public interface ICommentRepository : IBaseRepository<Comment>
{
  Task<List<Comment>> FindByTicketIdAsync(int ticketId, CancellationToken cancellationToken = default);
  Task<List<Comment>> FindByUserIdAsync(int userId, CancellationToken cancellationToken = default);
}

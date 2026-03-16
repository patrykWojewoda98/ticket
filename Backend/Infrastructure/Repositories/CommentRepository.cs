using System;
using Domain.Abstractions;
using Domain.Entities;
using Infrastructure.Context;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories;

public class CommentRepository : BaseRepository<Comment>, ICommentRepository
{
  public CommentRepository(DatabaseContext databaseContext) : base(databaseContext) { }

  public async Task<List<Comment>> FindByTicketIdAsync(int ticketId, CancellationToken cancellationToken = default)
  {
    return await _dbContext.Set<Comment>()
                 .Where(comment => comment.TicketId == ticketId)
                 .ToListAsync(cancellationToken);
  }

  public async Task<List<Comment>> FindByUserIdAsync(int userId, CancellationToken cancellationToken = default)
  {
    return await _dbContext.Set<Comment>()
                 .Where(comment => comment.UserId == userId)
                 .ToListAsync(cancellationToken);
  }
}

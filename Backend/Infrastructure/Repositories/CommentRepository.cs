using System;
using Domain.Abstractions;
using Domain.Entities;
using Infrastructure.Context;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories;

public class CommentRepository : BaseRepository<Comment>, ICommentRepository
{
  public CommentRepository(DatabaseContext databaseContext) : base(databaseContext) { }

  public async Task<List<Comment>> FindByTicketIdAsync(string ticketId)
  {
    return await _dbSet
                 .Where(comment => comment.TicketId == ticketId)
                 .ToListAsync();
  }

  public async Task<List<Comment>> FindByUserIdAsync(string userId)
  {
    return await _dbSet
                 .Where(comment => comment.UserId == userId)
                 .ToListAsync();
  }
}

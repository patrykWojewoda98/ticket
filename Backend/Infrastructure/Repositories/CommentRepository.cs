using System;
using Domain.Abstractions;
using Domain.Entities;
using Infrastructure.Context;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories;

public class CommentRepository : ICommentRepository
{
  private readonly DatabaseContext _context;

  public CommentRepository(DatabaseContext context)
  {
    _context = context;
  }

  public async Task<IEnumerable<Comment>> GetAllAsync()
  {
    return await _context.Comments.ToListAsync();
  }

  public async Task<Comment?> GetByIdAsync(string id)
  {
    return await _context.Comments.FindAsync(id);
  }

  public async Task<IEnumerable<Comment>> FindByTicketIdAsync(string ticketId)
  {
    return await _context.Comments
                         .Where(comment => comment.TicketId == ticketId)
                         .ToListAsync();
  }

  public async Task<IEnumerable<Comment>> FindByUserIdAsync(string userId)
  {
    return await _context.Comments
                         .Where(comment => comment.UserId == userId)
                         .ToListAsync();
  }

  public async Task<Comment> CreateAsync(Comment comment)
  {
    await _context.Comments.AddAsync(comment);
    await _context.SaveChangesAsync();
    return comment;
  }

  public async Task<Comment> UpdateAsync(string id, Comment comment)
  {
    var existingComment = await _context.Comments.FindAsync(id);
    if (existingComment != null)
    {
      existingComment.UserId = comment.UserId;
      existingComment.TicketId = comment.TicketId;
      existingComment.Content = comment.Content;
      existingComment.UpdatedAt = comment.UpdatedAt;

      _context.Comments.Update(existingComment);
      await _context.SaveChangesAsync();
      return existingComment;
    }
    return comment;
  }

  public async Task DeleteAsync(string id)
  {
    var existingComment = await _context.Comments.FindAsync(id);
    if (existingComment != null)
    {
      _context.Comments.Remove(existingComment);
      await _context.SaveChangesAsync();
    }
  }
}

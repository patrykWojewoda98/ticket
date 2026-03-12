using System;
using Domain.Entities;

namespace Domain.Abstractions;

public interface ICommentRepository
{
  Task<IEnumerable<Comment>> GetAllAsync();
  Task<Comment?> GetByIdAsync(string id);
  Task<IEnumerable<Comment>> FindByTicketIdAsync(string ticketId);
  Task<IEnumerable<Comment>> FindByUserIdAsync(string userId);
  Task<Comment> CreateAsync(Comment comment);
  Task<Comment> UpdateAsync(string id, Comment comment);
  Task DeleteAsync(string id);
}

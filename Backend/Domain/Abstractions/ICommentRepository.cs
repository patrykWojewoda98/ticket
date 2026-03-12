using System;
using Domain.Entities;

namespace Domain.Abstractions;

public interface ICommentRepository
{
  Task<List<Comment>> GetAllAsync();
  Task<Comment?> GetByIdAsync(string id);
  Task<List<Comment>> FindByTicketIdAsync(string ticketId);
  Task<List<Comment>> FindByUserIdAsync(string userId);
  Task<Comment> CreateAsync(Comment comment);
  Task<Comment> UpdateAsync(string id, Comment comment);
  Task DeleteAsync(string id);
}

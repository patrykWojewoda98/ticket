using System;
using Domain.Entities;

namespace Domain.Abstractions;

public interface ICommentRepository : IBaseRepository<Comment>
{
  Task<List<Comment>> FindByTicketIdAsync(string ticketId);
  Task<List<Comment>> FindByUserIdAsync(string userId);
}

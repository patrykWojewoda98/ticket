using System;
using Domain.Entities;

namespace Domain.Abstractions;

public interface ICommentRepository : IBaseRepository<Comment>
{
  Task<List<Comment>> FindByTicketIdAsync(int ticketId);
  Task<List<Comment>> FindByUserIdAsync(int userId);
}

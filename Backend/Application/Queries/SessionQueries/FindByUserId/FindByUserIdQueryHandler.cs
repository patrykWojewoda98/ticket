using System;
using Application.Dtos;
using Domain.Abstractions;
using MediatR;

namespace Application.Queries.SessionQueries.FindByUserId;

public class FindByUserIdQueryHandler : IRequestHandler<FindByUserIdQuery, List<SessionDto>>
{
  private readonly ISessionRepository _repository;

  public FindByUserIdQueryHandler(ISessionRepository repository)
  {
    _repository = repository;
  }

  public async Task<List<SessionDto>> Handle(FindByUserIdQuery request, CancellationToken cancellationToken)
  {
    var sessions = await _repository.FindByUserIdAsync(request.UserId);
    return sessions.Select(session => new SessionDto
    {
      Id = session.Id,
      UserId = session.UserId,
      Token = session.Token,
      ExpiresAt = session.ExpiresAt,
    }).ToList();
  }
}

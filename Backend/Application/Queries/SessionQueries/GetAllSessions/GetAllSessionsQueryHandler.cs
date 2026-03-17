using System;
using Application.Dtos;
using Domain.Abstractions;
using MediatR;

namespace Application.Queries.SessionQueries.GetAllSessions;

public class GetAllSessionsQueryHandler : IRequestHandler<GetAllSessionsQuery, List<SessionDto>>
{
  private readonly ISessionRepository _repository;

  public GetAllSessionsQueryHandler(ISessionRepository repository)
  {
    _repository = repository;
  }

  public async Task<List<SessionDto>> Handle(GetAllSessionsQuery request, CancellationToken cancellationToken)
  {
    var sessions = await _repository.GetAllAsync();
    return sessions.Select(session => new SessionDto
    {
      Id = session.Id,
      UserId = session.UserId,
      Token = session.Token,
      ExpiresAt = session.ExpiresAt
    }).ToList();
  }
}

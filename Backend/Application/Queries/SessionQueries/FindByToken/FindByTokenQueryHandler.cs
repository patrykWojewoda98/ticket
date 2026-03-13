using System;
using Application.Dtos;
using Domain.Abstractions;
using MediatR;

namespace Application.Queries.SessionQueries.FindByToken;

public class FindByTokenQueryHandler : IRequestHandler<FindByTokenQuery, SessionDto>
{
  private readonly ISessionRepository _repository;

  public FindByTokenQueryHandler(ISessionRepository repository)
  {
    _repository = repository;
  }

  public async Task<SessionDto> Handle(FindByTokenQuery request, CancellationToken cancellationToken)
  {
    var session = await _repository.FindByTokenAsync(request.Token);
    if (session == null) return null;

    return new SessionDto
    {
      Id = session.Id,
      UserId = session.UserId,
      Token = session.Token,
      ExpiresAt = session.ExpiresAt,
    };
  }
}

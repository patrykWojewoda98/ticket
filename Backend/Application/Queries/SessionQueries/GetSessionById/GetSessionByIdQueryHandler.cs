using System;
using Application.Dtos;
using Domain.Abstractions;
using MediatR;

namespace Application.Queries.SessionQueries.GetSessionById;

public class GetSessionByIdQueryHandler : IRequestHandler<GetSessionByIdQuery, SessionDto>
{
  private readonly ISessionRepository _repository;

  public GetSessionByIdQueryHandler(ISessionRepository repository)
  {
    _repository = repository;
  }

  public async Task<SessionDto> Handle(GetSessionByIdQuery request, CancellationToken cancellationToken)
  {
    var session = await _repository.GetByIdAsync(request.Id);
    if (session == null) return null;

    return new SessionDto
    {
      Id = session.Id,
      UserId = session.UserId,
      Token = session.Token,
      ExpiresAt = session.ExpiresAt
    };
  }
}

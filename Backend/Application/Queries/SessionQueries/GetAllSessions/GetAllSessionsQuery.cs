using System;
using Application.Dtos;
using MediatR;

namespace Application.Queries.SessionQueries.GetAllSessions;

public record GetAllSessionsQuery() : IRequest<List<SessionDto>>;

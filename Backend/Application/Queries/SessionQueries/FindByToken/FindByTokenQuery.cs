using System;
using Application.Dtos;
using MediatR;

namespace Application.Queries.SessionQueries.FindByToken;

public record FindByTokenQuery(string Token) : IRequest<SessionDto>;

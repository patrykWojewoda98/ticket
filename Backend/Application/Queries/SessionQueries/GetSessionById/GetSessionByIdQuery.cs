using System;
using Application.Dtos;
using MediatR;

namespace Application.Queries.SessionQueries.GetSessionById;

public record GetSessionByIdQuery(int Id) : IRequest<SessionDto>;

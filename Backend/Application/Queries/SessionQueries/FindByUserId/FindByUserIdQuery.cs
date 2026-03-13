using System;
using Application.Dtos;
using MediatR;

namespace Application.Queries.SessionQueries.FindByUserId;

public record FindByUserIdQuery(int UserId) : IRequest<List<SessionDto>>;

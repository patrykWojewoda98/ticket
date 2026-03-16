using System;
using Application.Dtos;
using Domain.Abstractions;
using MediatR;

namespace Application.Commands.SessionCommands.UpdateSession;

public class UpdateSessionCommandHandler : IRequestHandler<UpdateSessionCommand, SessionDto>
{
  private readonly ISessionRepository _repository;
  private readonly IUnitOfWorkService _unitOfWork;

  public UpdateSessionCommandHandler(ISessionRepository repository, IUnitOfWorkService unitOfWork)
  {
    _repository = repository;
    _unitOfWork = unitOfWork;
  }
  public async Task<SessionDto> Handle(UpdateSessionCommand request, CancellationToken cancellationToken)
  {
    var session = await _repository.GetByIdAsync(request.SessionId, cancellationToken);
    if (session == null) return null;

    session.Token = request.Token;
    session.IpAddress = request.IpAddress;
    session.UserAgent = request.UserAgent;
    session.ExpiresAt = request.ExpiresAt.Value;

    _repository.UpdateEntity(session);
    await _unitOfWork.SaveChangesAsync(cancellationToken);
    return new SessionDto
    {
      Id = session.Id,
      UserId = session.UserId,
      Token = session.Token,
      ExpiresAt = session.ExpiresAt
    };
  }
}

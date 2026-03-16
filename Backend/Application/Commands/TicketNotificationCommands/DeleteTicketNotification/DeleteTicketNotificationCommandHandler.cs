using System;

namespace Application.Commands.TicketNotificationCommands.DeleteTicketNotification;

public class DeleteTicketNotificationCommandHandler : IRequestHandler<DeleteSessionCommand, SessionDto>
{
  private readonly ISessionRepository _repository;
  private readonly IUnitOfWorkService _unitOfWork;

  public DeleteSessionCommandHandler(ISessionRepository repository, IUnitOfWorkService unitOfWork)
  {
    _repository = repository;
    _unitOfWork = unitOfWork;
  }

  public async Task<SessionDto> Handle(DeleteSessionCommand request, CancellationToken cancellationToken)
  {
    var session = await _repository.GetByIdAsync(request.SessionId, cancellationToken);
    if (session == null) return null;

    var SessionDto = new SessionDto
    {
      Id = session.Id,
      UserId = session.UserId,
      Token = session.Token,
      ExpiresAt = session.ExpiresAt
    };

    _repository.DeleteEntity(session);
    await _unitOfWork.SaveChangesAsync(cancellationToken);
    return SessionDto;
  }
}

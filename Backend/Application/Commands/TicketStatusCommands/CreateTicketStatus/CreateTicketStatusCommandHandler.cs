using System;

namespace Application.Commands.TicketStatusCommands.CreateTicketStatus;

public class CreateTicketStatusCommandHandler : IRequestHandler<CreateSessionCommand, SessionDto>
{
  private readonly ISessionRepository _repository;
  private readonly IUnitOfWorkService _unitOfWork;

  public CreateSessionCommandHandler(ISessionRepository repository, IUnitOfWorkService unitOfWork)
  {
    _repository = repository;
    _unitOfWork = unitOfWork;
  }

  public async Task<SessionDto> Handle(CreateSessionCommand request, CancellationToken cancellationToken)
  {
    var session = new Session
    {
      UserId = request.UserId,
      Token = request.Token,
      ExpiresAt = request.ExpiresAt,
      IpAddress = request.IpAddress,
      UserAgent = request.UserAgent
    };

    _repository.CreateEntity(session);
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


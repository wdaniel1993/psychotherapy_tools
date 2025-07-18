using TherapyTools.Domain.Common.Cqrs;

namespace TherapyTools.Domain.TherapyManagement.CommandHandlers.TherapySession;

public class CancelTherapySessionCommandHandler : IAggregateCommandHandler<CancelTherapySessionCommand, TherapySessionId, TherapySessionState>
{
    public Task<IEnumerable<IDomainEvent>> Handle(CancelTherapySessionCommand command, TherapySessionState state)
    {
        if (state.Status == TherapySessionStatus.Canceled)
            throw new InvalidOperationException("Cannot cancel a session that is already canceled.");
        if (state.Status == TherapySessionStatus.Done)
            throw new InvalidOperationException("Cannot cancel a session that is already done.");
        if (state.Status != TherapySessionStatus.Scheduled)
            throw new InvalidOperationException("Cannot cancel a session that is not scheduled.");
        return Task.FromResult<IEnumerable<IDomainEvent>>([new TherapySessionCanceled(command.Id)]);
    }
}
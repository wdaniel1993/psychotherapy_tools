using TherapyTools.Domain.Common.Cqrs;

namespace TherapyTools.Domain.TherapyManagement.CommandHandlers.TherapySession;

public class CompleteTherapySessionCommandHandler : IAggregateCommandHandler<CompleteTherapySessionCommand, TherapySessionId, TherapySessionState>
{
    public Task<IEnumerable<IDomainEvent>> Handle(CompleteTherapySessionCommand command, TherapySessionState state)
    {
        if (state.Status == TherapySessionStatus.Done)
            throw new InvalidOperationException("Cannot complete a session that is already done.");
        if (state.Status != TherapySessionStatus.Scheduled)
            throw new InvalidOperationException("Cannot complete a session that is not scheduled.");
        return Task.FromResult<IEnumerable<IDomainEvent>>([new TherapySessionCompleted(command.Id, command.Notes)]);
    }
}
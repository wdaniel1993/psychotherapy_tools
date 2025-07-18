using TherapyTools.Domain.Common.Cqrs;

namespace TherapyTools.Domain.TherapyManagement.CommandHandlers.TherapySession;

public class RescheduleTherapySessionCommandHandler : IAggregateCommandHandler<RescheduleTherapySessionCommand, TherapySessionId, TherapySessionState>
{
    public async IAsyncEnumerable<IDomainEvent> Handle(RescheduleTherapySessionCommand command, TherapySessionState state)
    {
        if (command.NewSlot.Start < DateTime.UtcNow)
            throw new InvalidOperationException("Cannot reschedule a session to a time in the past.");
        if (state.Status != TherapySessionStatus.Scheduled)
            throw new InvalidOperationException("Cannot reschedule a session that is not scheduled.");
        yield return new TherapySessionRescheduled(command.Id, command.NewSlot);
    }
}

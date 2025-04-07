using TherapyTools.Domain.Common.Cqrs;

namespace TherapyTools.Domain.TherapyManagement.CommandHandlers.TherapySession;

public class RescheduleTherapySessionCommandHandler : ICommandHandler<RescheduleTherapySessionCommand>
{
    private readonly IEventStore<TherapySessionId> _eventStore;

    public RescheduleTherapySessionCommandHandler(IEventStore<TherapySessionId> eventStore)
    {
        _eventStore = eventStore;
    }

    public async IAsyncEnumerable<IDomainEvent> Handle(RescheduleTherapySessionCommand command)
    {
        var currentState = await TherapySessionAggregate.GetCurrentState(_eventStore, command.Id);
        if (currentState.Status != TherapySessionStatus.Scheduled)
            throw new InvalidOperationException("Cannot reschedule a session that is not scheduled.");
        yield return new TherapySessionRescheduled(command.Id, command.NewSlot);
    }
}

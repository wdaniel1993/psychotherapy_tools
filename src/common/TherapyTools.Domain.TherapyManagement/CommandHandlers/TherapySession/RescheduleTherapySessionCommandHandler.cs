using TherapyTools.Domain.Common.Cqrs;

namespace TherapyTools.Domain.TherapyManagement.CommandHandlers.TherapySession;

public class RescheduleTherapySessionCommandHandler : ICommandHandler<RescheduleTherapySessionCommand>
{
    private readonly IEventStore _eventStore;

    public RescheduleTherapySessionCommandHandler(IEventStore eventStore)
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

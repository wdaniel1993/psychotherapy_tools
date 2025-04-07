using TherapyTools.Domain.Common.Cqrs;

namespace TherapyTools.Domain.TherapyManagement.CommandHandlers.TherapySession;

public class ScheduleTherapySessionCommandHandler : ICommandHandler<ScheduleTherapySessionCommand>
{
    private readonly IEventStore<TherapySessionId> _eventStore;

    public ScheduleTherapySessionCommandHandler(IEventStore<TherapySessionId> eventStore)
    {
        _eventStore = eventStore;
    }

    public async IAsyncEnumerable<IDomainEvent> Handle(ScheduleTherapySessionCommand command)
    {
        var currentState = await TherapySessionAggregate.GetCurrentState(_eventStore, command.Id);
        if(command.SessionTimeSlot.Start < DateTime.UtcNow)
            throw new InvalidOperationException("Cannot schedule a session in the past.");
        if (currentState.Status != TherapySessionStatus.Unconfirmed)
            throw new InvalidOperationException("Cannot schedule a session that is already scheduled or canceled.");
        yield return new TherapySessionScheduled(command.Id, command.SessionTimeSlot, command.Type, command.Notes);
    }
}

using TherapyTools.Domain.Common.Cqrs;

namespace TherapyTools.Domain.TherapyManagement.CommandHandlers.TherapySession;

public class CompleteTherapySessionCommandHandler : ICommandHandler<CompleteTherapySessionCommand>
{
    private readonly IEventStore<TherapySessionId> _eventStore;

    public CompleteTherapySessionCommandHandler(IEventStore<TherapySessionId> eventStore)
    {
        _eventStore = eventStore;
    }

    public async IAsyncEnumerable<IDomainEvent> Handle(CompleteTherapySessionCommand command)
    {
        var currentState = await TherapySessionAggregate.GetCurrentState(_eventStore, command.Id);
        if (currentState.Status == TherapySessionStatus.Done)
            throw new InvalidOperationException("Cannot complete a session that is already done.");
        if (currentState.Status != TherapySessionStatus.Scheduled)
            throw new InvalidOperationException("Cannot complete a session that is not scheduled.");
        yield return new TherapySessionCompleted(command.Id, command.Notes);
    }
}
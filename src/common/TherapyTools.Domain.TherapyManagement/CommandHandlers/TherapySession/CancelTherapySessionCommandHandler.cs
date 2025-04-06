using TherapyTools.Domain.Common.Cqrs;

namespace TherapyTools.Domain.TherapyManagement.CommandHandlers.TherapySession;

public class CancelTherapySessionCommandHandler : ICommandHandler<CancelTherapySessionCommand>
{
    private readonly IEventStore _eventStore;

    public CancelTherapySessionCommandHandler(IEventStore eventStore)
    {
        _eventStore = eventStore;
    }
    public async IAsyncEnumerable<IDomainEvent> Handle(CancelTherapySessionCommand command)
    {
        var currentState = await TherapySessionAggregate.GetCurrentState(_eventStore, command.Id);
        if (currentState.Status == TherapySessionStatus.Canceled)
            throw new InvalidOperationException("Cannot cancel a session that is already canceled.");
        if(currentState.Status == TherapySessionStatus.Done)
            throw new InvalidOperationException("Cannot cancel a session that is already done.");
        if (currentState.Status != TherapySessionStatus.Scheduled)
            throw new InvalidOperationException("Cannot cancel a session that is not scheduled.");
        yield return new TherapySessionCanceled(command.Id);
    }
}
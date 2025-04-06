using TherapyTools.Domain.Common.Cqrs;

namespace TherapyTools.Domain.TherapyManagement.CommandHandlers.TherapySession;

public class UpdateTherapySessionNotesCommandHandler : ICommandHandler<UpdateTherapySessionNotesCommand>
{
    private readonly IEventStore _eventStore;

    public UpdateTherapySessionNotesCommandHandler(IEventStore eventStore)
    {
        _eventStore = eventStore;
    }

    public async IAsyncEnumerable<IDomainEvent> Handle(UpdateTherapySessionNotesCommand command)
    {
        var currentState = await TherapySessionAggregate.GetCurrentState(_eventStore, command.Id);
        if(currentState.Status != TherapySessionStatus.Unconfirmed)
            throw new InvalidOperationException("Cannot update notes for a session that is not unconfirmed.");
        yield return new TherapySessionNotesUpdates(command.Id, command.Notes);
    }
}
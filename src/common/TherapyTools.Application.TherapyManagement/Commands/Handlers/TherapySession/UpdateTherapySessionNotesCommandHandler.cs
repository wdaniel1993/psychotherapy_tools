using TherapyTools.Application.Common;
using TherapyTools.Application.TherapyManagement.IntegrationEvents;
using TherapyTools.Domain.Common.Interfaces;
using TherapyTools.Domain.TherapyManagement;

namespace TherapyTools.Application.TherapyManagement.Commands.Handlers.TherapySession;

public class UpdateTherapySessionNotesCommandHandler(IEventStore<TherapySessionId> eventStore) : AbstractTherapySessionCommandHandler<UpdateTherapySessionNotesCommand>(eventStore)
{
    protected override Task<CommandResult> Handle(UpdateTherapySessionNotesCommand command, TherapySessionState state)
    {
        if (state.Status != TherapySessionStatus.Unconfirmed)
            throw new InvalidOperationException("Cannot update notes for a session that is not unconfirmed.");
        var domainEvent = new TherapySessionNotesUpdates(command.Id, command.Notes);
        var newState = TherapySessionAggregate.Apply(state, domainEvent);
        var integrationEvent = new TherapySessionIntegrationEvent(
            nameof(TherapySessionNotesUpdates),
            IntegrationEventType.Updated,
            domainEvent.ToModel(),
            newState.ToModel()
        );
        return Task.FromResult(new CommandResult([domainEvent], [integrationEvent]));
    }
}
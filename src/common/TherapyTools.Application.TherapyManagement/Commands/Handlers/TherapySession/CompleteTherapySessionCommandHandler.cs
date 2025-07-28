using TherapyTools.Application.Common;
using TherapyTools.Application.TherapyManagement.DataAccess;
using TherapyTools.Application.TherapyManagement.IntegrationEvents;
using TherapyTools.Domain.Common.Interfaces;
using TherapyTools.Domain.TherapyManagement;

namespace TherapyTools.Application.TherapyManagement.Commands.Handlers.TherapySession;

public class CompleteTherapySessionCommandHandler(IEventStore<TherapySessionId> eventStore) : AbstractTherapySessionCommandHandler<CompleteTherapySessionCommand>(eventStore)
{
    protected override Task<CommandResult> Handle(CompleteTherapySessionCommand command, TherapySessionState state)
    {
        if (state.Status == TherapySessionStatus.Done)
            throw new InvalidOperationException("Cannot complete a session that is already done.");
        if (state.Status != TherapySessionStatus.Scheduled)
            throw new InvalidOperationException("Cannot complete a session that is not scheduled.");
        var domainEvent = new TherapySessionCompleted(new TherapySessionId(command.Id), new SessionNotes(command.Notes));
        var newState = TherapySessionAggregate.Apply(state, domainEvent);
        var integrationEvent = new TherapySessionIntegrationEvent(
            nameof(TherapySessionCompleted),
            IntegrationEventType.Updated,
            domainEvent.ToModel(),
            newState.ToModel()
        );
        return Task.FromResult(new CommandResult([domainEvent], [integrationEvent]));
    }
}

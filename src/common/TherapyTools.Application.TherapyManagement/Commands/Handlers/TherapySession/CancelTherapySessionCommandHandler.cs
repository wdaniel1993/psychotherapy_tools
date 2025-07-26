using TherapyTools.Application.Common;
using TherapyTools.Application.TherapyManagement.IntegrationEvents;
using TherapyTools.Domain.Common.Interfaces;
using TherapyTools.Domain.TherapyManagement;

namespace TherapyTools.Application.TherapyManagement.Commands.Handlers.TherapySession;

public class CancelTherapySessionCommandHandler(IEventStore<TherapySessionId> eventStore) : AbstractTherapySessionCommandHandler<CancelTherapySessionCommand>(eventStore)
{
    protected override Task<CommandResult> Handle(CancelTherapySessionCommand command, TherapySessionState state)
    {
        if (state.Status == TherapySessionStatus.Canceled)
            throw new InvalidOperationException("Session is already canceled.");
        var domainEvent = new TherapySessionCanceled(command.Id);
        var newState = TherapySessionAggregate.Apply(state, domainEvent);
        var integrationEvent = new TherapySessionIntegrationEvent(
            nameof(TherapySessionCanceled),
            IntegrationEventType.Deleted,
            domainEvent.ToModel(),
            newState.ToModel()
        );
        return Task.FromResult(new CommandResult([domainEvent], [integrationEvent]));
    }
}

using TherapyTools.Application.Common;
using TherapyTools.Application.TherapyManagement.DataAccess;
using TherapyTools.Application.TherapyManagement.IntegrationEvents;
using TherapyTools.Domain.Common.Interfaces;
using TherapyTools.Domain.TherapyManagement;

namespace TherapyTools.Application.TherapyManagement.Commands.Handlers.TherapySession;

public class RescheduleTherapySessionCommandHandler(IEventStore<TherapySessionId> eventStore) : AbstractTherapySessionCommandHandler<RescheduleTherapySessionCommand>(eventStore)
{
    protected override Task<CommandResult> Handle(RescheduleTherapySessionCommand command, TherapySessionState state)
    {
        if (command.NewSlot.Start < DateTime.UtcNow)
            throw new InvalidOperationException("Cannot reschedule a session to a time in the past.");
        if (state.Status != TherapySessionStatus.Scheduled)
            throw new InvalidOperationException("Cannot reschedule a session that is not scheduled.");
        var domainEvent = new TherapySessionRescheduled(new TherapySessionId(command.Id), command.NewSlot.ToDomain());
        var newState = TherapySessionAggregate.Apply(state, domainEvent);
        var integrationEvent = new TherapySessionIntegrationEvent(
            nameof(TherapySessionRescheduled),
            IntegrationEventType.Updated,
            domainEvent.ToModel(),
            newState.ToModel()
        );
        return Task.FromResult(new CommandResult([domainEvent], [integrationEvent]));
    }
}

using TherapyTools.Application.Common;
using TherapyTools.Application.TherapyManagement.DataAccess;
using TherapyTools.Application.TherapyManagement.IntegrationEvents;
using TherapyTools.Domain.Common.Interfaces;
using TherapyTools.Domain.TherapyManagement;

namespace TherapyTools.Application.TherapyManagement.Commands.Handlers.TherapySession;

public class ScheduleTherapySessionCommandHandler(IEventStore<TherapySessionId> eventStore) : AbstractTherapySessionCommandHandler<ScheduleTherapySessionCommand>(eventStore)
{
    protected override Task<CommandResult> Handle(ScheduleTherapySessionCommand command, TherapySessionState state)
    {
        if(command.SessionTimeSlot.Start < DateTime.UtcNow)
            throw new InvalidOperationException("Cannot schedule a session in the past.");
        if (state.Status != TherapySessionStatus.Unconfirmed)
            throw new InvalidOperationException("Cannot schedule a session that is already scheduled or canceled.");
        var domainEvent = new TherapySessionScheduled(command.Id, command.SessionTimeSlot, command.Type, command.Notes);
        var newState = TherapySessionAggregate.Apply(state, domainEvent);
        var integrationEvent = new TherapySessionIntegrationEvent(
            nameof(TherapySessionScheduled),
            IntegrationEventType.Created,
            domainEvent.ToModel(),
            newState.ToModel()
        );
        return Task.FromResult(new CommandResult([domainEvent], [integrationEvent]));
    }
}

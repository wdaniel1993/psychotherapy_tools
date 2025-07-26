using TherapyTools.Application.Common;
using TherapyTools.Application.TherapyManagement.IntegrationEvents;
using TherapyTools.Domain.Common.Interfaces;
using TherapyTools.Domain.TherapyManagement;

namespace TherapyTools.Application.TherapyManagement.Commands.Handlers.TherapyPlan;

public class ActivateTherapyPlanCommandHandler(IEventStore<TherapyPlanId> eventStore) : AbstractTherapyPlanCommandHandler<ActivateTherapyPlanCommand>(eventStore)
{
    protected override Task<CommandResult> Handle(ActivateTherapyPlanCommand command, TherapyPlanState state)
    {
        if(state.Status != TherapyPlanStatus.Draft)
            throw new InvalidOperationException("Therapy plan must be in draft status to be activated.");
        var domainEvent = new TherapyPlanActivated(command.Id);
        var newState = TherapyPlanAggregate.Apply(state, domainEvent);
        var integrationEvent = new TherapyPlanIntegrationEvent(
            nameof(TherapyPlanActivated),
            IntegrationEventType.Updated,
            domainEvent.ToModel(),
            newState.ToModel()
        );
        return Task.FromResult(new CommandResult([domainEvent], [integrationEvent]));
    }
}

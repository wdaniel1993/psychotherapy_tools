using TherapyTools.Application.Common;
using TherapyTools.Application.TherapyManagement.DataAccess;
using TherapyTools.Application.TherapyManagement.IntegrationEvents;
using TherapyTools.Domain.Common.Interfaces;
using TherapyTools.Domain.TherapyManagement;

namespace TherapyTools.Application.TherapyManagement.Commands.Handlers.TherapyPlan;

public class CompleteTherapyPlanCommandHandler(IEventStore<TherapyPlanId> eventStore) : AbstractTherapyPlanCommandHandler<CompleteTherapyPlanCommand>(eventStore)
{
    protected override Task<CommandResult> Handle(CompleteTherapyPlanCommand command, TherapyPlanState state)
    {
        if(state.Status != TherapyPlanStatus.Active)
            throw new InvalidOperationException("Therapy plan must be in active status to be completed.");
        var domainEvent = new TherapyPlanCompleted(command.Id);
        var newState = TherapyPlanAggregate.Apply(state, domainEvent);
        var integrationEvent = new TherapyPlanIntegrationEvent(
            nameof(TherapyPlanCompleted),
            IntegrationEventType.Updated,
            domainEvent.ToModel(),
            newState.ToModel()
        );
        return Task.FromResult(new CommandResult([domainEvent], [integrationEvent]));
    }
}

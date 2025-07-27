using TherapyTools.Application.Common;
using TherapyTools.Application.TherapyManagement.DataAccess;
using TherapyTools.Application.TherapyManagement.IntegrationEvents;
using TherapyTools.Domain.Common.Interfaces;
using TherapyTools.Domain.TherapyManagement;

namespace TherapyTools.Application.TherapyManagement.Commands.Handlers.TherapyPlan;

public class DiscardTherapyPlanCommandHandler(IEventStore<TherapyPlanId> eventStore) : AbstractTherapyPlanCommandHandler<DiscardTherapyPlanCommand>(eventStore)
{
    protected override Task<CommandResult> Handle(DiscardTherapyPlanCommand command, TherapyPlanState state)
    {
        if(state.Status == TherapyPlanStatus.Discarded)
            throw new InvalidOperationException("Therapy plan is already discarded.");
        var domainEvent = new TherapyPlanDiscarded(command.Id);
        var newState = TherapyPlanAggregate.Apply(state, domainEvent);
        var integrationEvent = new TherapyPlanIntegrationEvent(
            nameof(TherapyPlanDiscarded),
            IntegrationEventType.Deleted,
            domainEvent.ToModel(),
            newState.ToModel()
        );
        return Task.FromResult(new CommandResult([domainEvent], [integrationEvent]));
    }
}

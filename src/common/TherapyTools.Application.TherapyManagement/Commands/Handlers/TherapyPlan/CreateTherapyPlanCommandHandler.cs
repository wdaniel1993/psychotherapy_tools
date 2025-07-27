using TherapyTools.Application.Common;
using TherapyTools.Application.TherapyManagement.DataAccess;
using TherapyTools.Application.TherapyManagement.IntegrationEvents;
using TherapyTools.Domain.Common.Interfaces;
using TherapyTools.Domain.TherapyManagement;

namespace TherapyTools.Application.TherapyManagement.Commands.Handlers.TherapyPlan;

public class CreateTherapyPlanCommandHandler(IEventStore<TherapyPlanId> eventStore) : AbstractTherapyPlanCommandHandler<CreateTherapyPlanCommand>(eventStore)
{
    protected override Task<CommandResult> Handle(CreateTherapyPlanCommand command, TherapyPlanState state)
    {
        var domainEvent = new TherapyPlanCreated(command.Id, command.GoalList, command.Description);
        var newState = TherapyPlanAggregate.Apply(state, domainEvent);
        var integrationEvent = new TherapyPlanIntegrationEvent(
            nameof(TherapyPlanCreated),
            IntegrationEventType.Created,
            domainEvent.ToModel(),
            newState.ToModel()
        );
        return Task.FromResult(new CommandResult([domainEvent], [integrationEvent]));
    }
}
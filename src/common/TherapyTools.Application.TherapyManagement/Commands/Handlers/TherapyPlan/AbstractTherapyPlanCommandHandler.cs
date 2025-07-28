using TherapyTools.Application.Common;
using TherapyTools.Application.Common.Interfaces;
using TherapyTools.Domain.Common.Interfaces;
using TherapyTools.Domain.TherapyManagement;

namespace TherapyTools.Application.TherapyManagement.Commands.Handlers.TherapyPlan;

public abstract class AbstractTherapyPlanCommandHandler<TCommand>(IEventStore<TherapyPlanId> eventStore) : AggregateCommandHandler<TCommand, TherapyPlanId, TherapyPlanState>(eventStore)
    where TCommand : IAggregateCommand
{
    protected override TherapyPlanState CreateStateFromEvents(IEnumerable<IDomainEvent> events)
        => TherapyPlanAggregate.Replay(events);

    protected override TherapyPlanId FromGuid(Guid id) => new(id);
}

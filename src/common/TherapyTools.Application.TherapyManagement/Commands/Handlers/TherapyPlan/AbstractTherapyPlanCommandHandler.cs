using System.Collections.Generic;
using TherapyTools.Application.Common;
using TherapyTools.Application.Common.Interfaces;
using TherapyTools.Domain.Common.Interfaces;
using TherapyTools.Domain.TherapyManagement;

namespace TherapyTools.Application.TherapyManagement.Commands.Handlers.TherapyPlan;

public abstract class AbstractTherapyPlanCommandHandler<TCommand>(IEventStore<TherapyPlanId> eventStore) : AggregateCommandHandler<TCommand, TherapyPlanId, TherapyPlanState>(eventStore)
    where TCommand : IAggregateCommand<TherapyPlanId>
{
    protected override TherapyPlanState CreateStateFromEvents(IEnumerable<IDomainEvent> events)
        => TherapyPlanAggregate.Replay(events);
}

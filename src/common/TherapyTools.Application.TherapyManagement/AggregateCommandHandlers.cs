using System.Collections.Generic;
using TherapyTools.Application.Common;
using TherapyTools.Application.Common.Interfaces;
using TherapyTools.Domain.Common.Interfaces;
using TherapyTools.Domain.TherapyManagement;

namespace TherapyTools.Application.TherapyManagement;

public abstract class TherapyPlanAggregateCommandHandler<TCommand> : AggregateCommandHandler<TCommand, TherapyPlanId, TherapyPlanState>
    where TCommand : IAggregateCommand<TherapyPlanId>
{
    protected TherapyPlanAggregateCommandHandler(IEventStore<TherapyPlanId> eventStore) : base(eventStore) { }
    protected override TherapyPlanState CreateStateFromEvents(IEnumerable<IDomainEvent> events)
        => TherapyPlanAggregate.Replay(events);
}

public abstract class TherapySessionAggregateCommandHandler<TCommand> : AggregateCommandHandler<TCommand, TherapySessionId, TherapySessionState>
    where TCommand : IAggregateCommand<TherapySessionId>
{
    protected TherapySessionAggregateCommandHandler(IEventStore<TherapySessionId> eventStore) : base(eventStore) { }
    protected override TherapySessionState CreateStateFromEvents(IEnumerable<IDomainEvent> events)
        => TherapySessionAggregate.Replay(events);
}

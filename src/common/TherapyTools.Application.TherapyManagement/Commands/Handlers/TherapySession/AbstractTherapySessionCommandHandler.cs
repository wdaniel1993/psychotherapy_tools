using System.Collections.Generic;
using TherapyTools.Application.Common;
using TherapyTools.Application.Common.Interfaces;
using TherapyTools.Domain.Common.Interfaces;
using TherapyTools.Domain.TherapyManagement;

namespace TherapyTools.Application.TherapyManagement.Commands.Handlers.TherapySession;

public abstract class AbstractTherapySessionCommandHandler<TCommand>(IEventStore<TherapySessionId> eventStore)
    : AggregateCommandHandler<TCommand, TherapySessionId, TherapySessionState>(eventStore)
    where TCommand : IAggregateCommand<TherapySessionId>
{
    protected override TherapySessionState CreateStateFromEvents(IEnumerable<IDomainEvent> events)
        => TherapySessionAggregate.Replay(events);
}

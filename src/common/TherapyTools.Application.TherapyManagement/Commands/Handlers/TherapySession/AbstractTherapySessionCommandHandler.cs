using TherapyTools.Application.Common;
using TherapyTools.Application.Common.Interfaces;
using TherapyTools.Domain.Common.Interfaces;
using TherapyTools.Domain.TherapyManagement;

namespace TherapyTools.Application.TherapyManagement.Commands.Handlers.TherapySession;

public abstract class AbstractTherapySessionCommandHandler<TCommand>(IEventStore<TherapySessionId> eventStore)
    : AggregateCommandHandler<TCommand, TherapySessionId, TherapySessionState>(eventStore)
    where TCommand : IAggregateCommand
{
    protected override TherapySessionState CreateStateFromEvents(IEnumerable<IDomainEvent> events)
        => TherapySessionAggregate.Replay(events);

    protected override TherapySessionId FromGuid(Guid id) => new(id);
}

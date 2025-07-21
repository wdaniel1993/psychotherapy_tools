using Mediator;
using TherapyTools.Application.Common.Interfaces;
using TherapyTools.Domain.Common;
using TherapyTools.Domain.Common.Interfaces;

namespace TherapyTools.Application.Common;

public abstract class AggregateCommandHandler<TCommand, TAggregateId, TAggregateState>(IEventStore<TAggregateId> eventStore) : ICommandHandler<TCommand, CommandResult>
    where TCommand : IAggregateCommand<TAggregateId>
    where TAggregateId : IAggregateId
    where TAggregateState : AggregateState<TAggregateId>
{
    private readonly IEventStore<TAggregateId> _eventStore = eventStore;

    public async ValueTask<CommandResult> Handle(TCommand command, CancellationToken cancellationToken)
    {
        var events = await _eventStore.GetEvents(command.AggregateId);
        var state = CreateStateFromEvents(events);
        return await Handle(command, state);
    }

    protected abstract TAggregateState CreateStateFromEvents(IEnumerable<IDomainEvent> events);
    protected abstract Task<CommandResult> Handle(TCommand command, TAggregateState state);
}
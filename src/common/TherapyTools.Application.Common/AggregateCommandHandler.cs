using TherapyTools.Application.Common.Interfaces;
using TherapyTools.Domain.Common.Interfaces;

namespace TherapyTools.Application.Common;

public abstract class AggregateCommandHandler<TCommand, TAggregateId, TAggregateState> : ICommandHandler<TCommand>
    where TCommand : IAggregateCommand<TAggregateId>
    where TAggregateId : IAggregateId
    where TAggregateState : AggregateState<TAggregateId>
{
    private readonly IEventStore<TAggregateId> _eventStore;

    protected AggregateCommandHandler(IEventStore<TAggregateId> eventStore)
    {
        _eventStore = eventStore;
    }

    public async Task<CommandDispatchResult> Handle(TCommand command)
    {
        var events = await _eventStore.GetEvents(command.AggregateId);
        var state = CreateStateFromEvents(events);
        return await Handle(command, state);
    }

    protected abstract TAggregateState CreateStateFromEvents(IEnumerable<IDomainEvent> events);
    protected abstract Task<CommandDispatchResult> Handle(TCommand command, TAggregateState state);
}
namespace TherapyTools.Domain.Common.Cqrs;
public interface ICommandHandler<TCommand, TState> where TCommand : IDomainCommand
{
    Task<IEnumerable<IDomainEvent>> Handle(TCommand command, TState state);
}

public interface IAggregateCommandHandler<TCommand, TAggregateId, TAggregateState> : ICommandHandler<TCommand, TAggregateState>
    where TCommand : IAggregateCommand<TAggregateId>
    where TAggregateId : IAggregateId
    where TAggregateState : AggregateState<TAggregateId>
{ }

public interface IMulipleAggregateCommandHandler : ICommandHandler<MultiAggregateCommand, IEnumerable<AggregateState<IAggregateId>>>
{ }
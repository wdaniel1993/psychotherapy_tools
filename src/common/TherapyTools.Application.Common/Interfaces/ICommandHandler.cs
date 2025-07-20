using TherapyTools.Domain.Common.Interfaces;

namespace TherapyTools.Application.Common.Interfaces;
public interface ICommandHandler<TCommand, TState> where TCommand : ICommand
{
    Task<IEnumerable<IDomainEvent>> Handle(TCommand command, TState state);
}

public interface IAggregateCommandHandler<TCommand, TAggregateId, TAggregateState> : ICommandHandler<TCommand, TAggregateState>
    where TCommand : IAggregateCommand<TAggregateId>
    where TAggregateId : IAggregateId
    where TAggregateState : AggregateState<TAggregateId>
{ }
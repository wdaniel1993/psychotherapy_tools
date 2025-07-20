using TherapyTools.Domain.Common.Interfaces;

namespace TherapyTools.Application.Common.Interfaces;
public interface ICommandHandler<TCommand> where TCommand : ICommand
{
    Task<IEnumerable<IDomainEvent>> Handle(TCommand command);
}

public interface IAggregateCommandHandler<TCommand, TAggregateId, TAggregateState> : ICommandHandler<TCommand>
    where TCommand : IAggregateCommand<TAggregateId>
    where TAggregateId : IAggregateId
    where TAggregateState : AggregateState<TAggregateId>
{ }
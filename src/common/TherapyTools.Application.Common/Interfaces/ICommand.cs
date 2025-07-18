using TherapyTools.Domain.Common.Interfaces;

namespace TherapyTools.Application.Common.Interfaces;

public interface ICommand {}
public interface IAggregateCommand<TAggregateId> : ICommand
    where TAggregateId : IAggregateId
{ 
    TAggregateId AggregateId { get; }
}

public abstract record AggregateCommand<TAggregateId>(TAggregateId Id) : IAggregateCommand<TAggregateId>
    where TAggregateId : IAggregateId { 
    public TAggregateId AggregateId => Id;
}

public abstract record MultiAggregateCommand(IEnumerable<IAggregateId> Ids) : ICommand
{
    public IEnumerable<IAggregateId> AggregateIds => Ids;
}
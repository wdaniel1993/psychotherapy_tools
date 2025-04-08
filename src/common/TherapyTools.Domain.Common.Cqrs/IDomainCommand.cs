namespace TherapyTools.Domain.Common.Cqrs;

public interface IDomainCommand {}
public interface IAggregateCommand<TAggregateId> : IDomainCommand
    where TAggregateId : IAggregateId
{ 
    TAggregateId AggregateId { get; }
}

public abstract record AggregateCommand<TAggregateId>(TAggregateId Id) : IAggregateCommand<TAggregateId>
    where TAggregateId : IAggregateId { 
    public TAggregateId AggregateId => Id;
}
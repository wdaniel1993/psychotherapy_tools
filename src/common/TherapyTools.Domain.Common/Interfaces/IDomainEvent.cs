namespace TherapyTools.Domain.Common.Interfaces;

public interface IDomainEvent { }

public interface IAggregateDomainEvent<TAggregateId> : IDomainEvent
    where TAggregateId : IAggregateId
{ 
    TAggregateId AggregateId { get; }
}

public abstract record AggregateDomainEvent<TAggregateId>(TAggregateId AggregateId) : IAggregateDomainEvent<TAggregateId>
    where TAggregateId : IAggregateId { 
}
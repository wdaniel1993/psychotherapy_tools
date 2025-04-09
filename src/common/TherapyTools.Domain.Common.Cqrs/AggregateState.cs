namespace TherapyTools.Domain.Common.Cqrs;

public abstract record AggregateState<TAggregateId>(TAggregateId Id)
    where TAggregateId : IAggregateId
{ }
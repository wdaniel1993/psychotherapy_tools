namespace TherapyTools.Domain.Common.Interfaces;

public abstract record AggregateState<TAggregateId>(TAggregateId Id)
    where TAggregateId : IAggregateId
{ }
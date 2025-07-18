namespace TherapyTools.Domain.Common.Cqrs;

public interface IEventStore<TAggregateId>
    where TAggregateId : IAggregateId
{
    Task Save(IAggregateDomainEvent<TAggregateId> domainEvent);
    Task<IEnumerable<IDomainEvent>> GetEvents(TAggregateId aggregateId);
}
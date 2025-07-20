namespace TherapyTools.Domain.Common.Interfaces;

public interface IEventStore<TAggregateId>
    where TAggregateId : IAggregateId
{
    Task Append(IDomainEvent domainEvent);
    Task<IEnumerable<IDomainEvent>> GetEvents(TAggregateId aggregateId);
}
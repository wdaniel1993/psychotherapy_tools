namespace TherapyTools.Domain.Common.Interfaces;

public interface IEventStore<TAggregateId>
    where TAggregateId : IAggregateId
{
    Task Append(IEnumerable<IDomainEvent> domainEvent);
    Task<IEnumerable<IDomainEvent>> GetEvents(TAggregateId aggregateId);
}
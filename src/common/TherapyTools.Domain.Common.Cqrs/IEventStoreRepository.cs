namespace TherapyTools.Domain.Common.Cqrs;

public interface IEventStoreRepository
{
    Task<IEnumerable<IDomainEvent>> GetEventsAsync(Guid aggregateId);
    Task SaveEventsAsync(Guid aggregateId, IEnumerable<IDomainEvent> events);
}

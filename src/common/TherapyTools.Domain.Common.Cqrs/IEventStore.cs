namespace TherapyTools.Domain.Common.Cqrs;

public interface IEventStore
{
    Task Save(IDomainEvent domainEvent);
    Task<IEnumerable<IDomainEvent>> GetEvents(Guid aggregateId);
}
namespace TherapyTools.Domain.Common.Cqrs;

public interface IEventStore<TIdentifier>
{
    Task Save(IDomainEvent domainEvent);
    Task<IEnumerable<IDomainEvent>> GetEvents(TIdentifier aggregateId);
}
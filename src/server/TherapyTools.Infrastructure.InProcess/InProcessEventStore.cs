using System.Collections.Concurrent;
using TherapyTools.Domain.Common.Interfaces;

namespace TherapyTools.Infrastructure.InProcess;

public class InProcessEventStore<TAggregateId> : IEventStore<TAggregateId>
    where TAggregateId : IAggregateId
{
    private readonly ConcurrentDictionary<Guid, List<IDomainEvent>> _store = new();

    public Task Append(IDomainEvent domainEvent)
    {
        if (domainEvent is IAggregateDomainEvent<TAggregateId> aggregateEvent)
        {
            var key = aggregateEvent.AggregateId.ToGuid();
            _store.AddOrUpdate(key,
                _ => new List<IDomainEvent> { domainEvent },
                (_, list) => { list.Add(domainEvent); return list; });
        }
        return Task.CompletedTask;
    }

    public Task<IEnumerable<IDomainEvent>> GetEvents(TAggregateId aggregateId)
    {
        var key = aggregateId.ToGuid();
        _store.TryGetValue(key, out var events);
        return Task.FromResult(events?.AsEnumerable() ?? []);
    }
}

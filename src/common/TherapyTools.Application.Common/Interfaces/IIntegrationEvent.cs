using Mediator;
using TherapyTools.Domain.Common.Interfaces;

namespace TherapyTools.Application.Common.Interfaces;

public enum IntegrationEventType
{
    Created,
    Updated,
    Deleted,
    Refresh
}

public interface IIntegrationEvent : INotification
{
    string EventName { get; }
    IntegrationEventType EventType { get; }
}

public interface IAggregateIntegrationEvent<TAggregateId> : IIntegrationEvent
    where TAggregateId : IAggregateId
{
}

public abstract record AggregateIntegrationEvent<TAggregateId>(
    string EventName,
    IntegrationEventType EventType,
    IAggregateDomainEvent<TAggregateId>? Event,
    AggregateState<TAggregateId>? State
) : IAggregateIntegrationEvent<TAggregateId>
    where TAggregateId : IAggregateId
{
}
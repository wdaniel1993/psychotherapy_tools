using TherapyTools.Domain.Common.Interfaces;

namespace TherapyTools.Application.Common.Interfaces;


public enum IntegrationEventType
{
    Created,
    Updated,
    Deleted,
    Refresh
}


public interface IIntegrationEvent
{
    string EventName { get; }
    IntegrationEventType EventType { get; }
}

public interface IAggregateIntegrationEvent<TAggregateId> : IIntegrationEvent
    where TAggregateId : IAggregateId
{
}

public record AggregateIntegrationEvent<TAggregateId>(string EventName, IntegrationEventType EventType, IAggregateDomainEvent<TAggregateId>? Event, AggregateState<TAggregateId>? State) : IAggregateIntegrationEvent<TAggregateId>
    where TAggregateId : IAggregateId
{

}
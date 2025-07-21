using TherapyTools.Application.Common.Interfaces;
using TherapyTools.Domain.Common;
using TherapyTools.Domain.Common.Interfaces;

namespace TherapyTools.Application.Common;

public abstract record AggregateIntegrationEvent<TAggregateId>(
    string EventName,
    IntegrationEventType EventType,
    IAggregateDomainEvent<TAggregateId>? Event,
    AggregateState<TAggregateId>? State
) : IAggregateIntegrationEvent<TAggregateId>
    where TAggregateId : IAggregateId
{
    public TAggregateId AggregateId
    {
        get
        {
            if (Event is not null)
                return Event.AggregateId;
            if (State is not null)
                return State.AggregateId;
            throw new InvalidOperationException("AggregateId is not set in the event or state.");
        }
    }
}
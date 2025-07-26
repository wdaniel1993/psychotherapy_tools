using TherapyTools.Application.Common.Interfaces;
using TherapyTools.Domain.Common;
using TherapyTools.Domain.Common.Interfaces;

namespace TherapyTools.Application.Common;

public abstract record AggregateIntegrationEvent<TAggregateId>(
    string EventName,
    IntegrationEventType EventType,
    IAggregateEventModel? Event,
    AggregateState<TAggregateId>? State
) : IAggregateIntegrationEvent<TAggregateId>
    where TAggregateId : IAggregateId
{
    public Guid AggregateId
    {
        get
        {
            if (Event is not null)
                return Event.Id;
            if (State is not null)
                return State.AggregateId;
            throw new InvalidOperationException("AggregateId is not set in the event or state.");
        }
    }
}
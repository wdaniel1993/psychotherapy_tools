using TherapyTools.Application.Common.Interfaces;

namespace TherapyTools.Application.Common;

public abstract record AggregateIntegrationEvent<TModel>(
    string EventName,
    IntegrationEventType EventType,
    IAggregateEventModel? Event,
    TModel? State
) : IAggregateIntegrationEvent
    where TModel : IAggregateModel
{
    public Guid AggregateId
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
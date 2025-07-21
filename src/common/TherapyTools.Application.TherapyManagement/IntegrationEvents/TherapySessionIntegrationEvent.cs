using TherapyTools.Application.Common;
using TherapyTools.Domain.Common;
using TherapyTools.Domain.Common.Interfaces;
using TherapyTools.Domain.TherapyManagement;

namespace TherapyTools.Application.TherapyManagement.IntegrationEvents;

public record TherapySessionIntegrationEvent(
    string EventName,
    IntegrationEventType EventType,
    IAggregateDomainEvent<TherapySessionId>? Event,
    AggregateState<TherapySessionId>? State
) : AggregateIntegrationEvent<TherapySessionId>(EventName, EventType, Event, State);

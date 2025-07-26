using TherapyTools.Application.Common;
using TherapyTools.Application.Common.Interfaces;

namespace TherapyTools.Application.TherapyManagement.IntegrationEvents;

public record TherapySessionIntegrationEvent(
    string EventName,
    IntegrationEventType EventType,
    IAggregateEventModel? Event,
    TherapySessionModel? State
) : AggregateIntegrationEvent<TherapySessionModel>(EventName, EventType, Event, State);

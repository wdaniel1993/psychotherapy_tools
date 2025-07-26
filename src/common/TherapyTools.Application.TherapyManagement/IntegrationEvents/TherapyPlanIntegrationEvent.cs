using TherapyTools.Application.Common;
using TherapyTools.Application.Common.Interfaces;

namespace TherapyTools.Application.TherapyManagement.IntegrationEvents;

public record TherapyPlanIntegrationEvent(
    string EventName,
    IntegrationEventType EventType,
    IAggregateEventModel Event,
    TherapyPlanModel? State
) : AggregateIntegrationEvent<TherapyPlanModel>(EventName, EventType, Event, State);

using TherapyTools.Application.Common;
using TherapyTools.Application.Common.Interfaces;
using TherapyTools.Application.TherapyManagement.DataAccess;

namespace TherapyTools.Application.TherapyManagement.IntegrationEvents;

public record TherapyPlanIntegrationEvent(
    string EventName,
    IntegrationEventType EventType,
    IAggregateEventModel Event,
    TherapyPlanModel? State
) : AggregateIntegrationEvent<TherapyPlanModel>(EventName, EventType, Event, State);

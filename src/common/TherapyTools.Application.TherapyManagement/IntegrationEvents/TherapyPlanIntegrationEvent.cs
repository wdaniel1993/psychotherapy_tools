using TherapyTools.Application.Common;
using TherapyTools.Domain.Common;
using TherapyTools.Domain.Common.Interfaces;
using TherapyTools.Domain.TherapyManagement;

namespace TherapyTools.Application.TherapyManagement.IntegrationEvents;

public record TherapyPlanIntegrationEvent(
    string EventName,
    IntegrationEventType EventType,
    IAggregateDomainEvent<TherapyPlanId>? Event,
    AggregateState<TherapyPlanId>? State
) : AggregateIntegrationEvent<TherapyPlanId>(EventName, EventType, Event, State);

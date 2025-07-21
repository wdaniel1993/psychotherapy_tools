using TherapyTools.Domain.TherapyManagement;
using TherapyTools.Domain.Common.Interfaces;
using Mediator;
using TherapyTools.Domain.Common;
using TherapyTools.Application.Common;

var builder = WebApplication.CreateBuilder(args);

// Add GraphQL services
builder.Services
    .AddGraphQLServer()
    .AddQueryType<Query>()
    .AddMutationType<Mutation>()
    .AddSubscriptionType<Subscription>()
    .AddInMemorySubscriptions();

var app = builder.Build();

app.UseRouting();
app.UseWebSockets();
app.MapGraphQL();

app.Run();

// Integration event types for GraphQL subscriptions
public record TherapyPlanIntegrationEvent(
    string EventName,
    IntegrationEventType EventType,
    IAggregateDomainEvent<TherapyPlanId>? Event,
    AggregateState<TherapyPlanId>? State
) : AggregateIntegrationEvent<TherapyPlanId>(EventName, EventType, Event, State);

public record TherapySessionIntegrationEvent(
    string EventName,
    IntegrationEventType EventType,
    IAggregateDomainEvent<TherapySessionId>? Event,
    AggregateState<TherapySessionId>? State
) : AggregateIntegrationEvent<TherapySessionId>(EventName, EventType, Event, State);

// Placeholder mutation and subscription types
public class Mutation;

public class Subscription
{
    [Subscribe]
    [Topic($"TherapyPlan_{{{nameof(id)}}}")]
    public TherapyPlanIntegrationEvent OnTherapyPlanChanged(TherapyPlanId id, [EventMessage] TherapyPlanIntegrationEvent @event)
        => @event;

    [Subscribe]
    [Topic($"TherapySession_{{{nameof(id)}}}")]
    public TherapySessionIntegrationEvent OnTherapySessionChanged(TherapySessionId id, [EventMessage] TherapySessionIntegrationEvent @event)
        => @event;

}

// Query type for TherapyPlan and TherapySession
public class Query
{
    public async Task<TherapyPlanState?> GetTherapyPlanById(
        TherapyPlanId id,
        [Service] IEventStore<TherapyPlanId> eventStore)
    {
        return await TherapyPlanAggregate.GetCurrentState(eventStore, id);
    }

    public async Task<TherapySessionState?> GetTherapySessionById(
        TherapySessionId id,
        [Service] IEventStore<TherapySessionId> eventStore)
    {
        return await TherapySessionAggregate.GetCurrentState(eventStore, id);
    }
}

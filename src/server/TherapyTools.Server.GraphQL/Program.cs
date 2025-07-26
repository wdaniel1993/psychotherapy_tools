using TherapyTools.Infrastructure.InProcess;
using TherapyTools.Domain.TherapyManagement;
using TherapyTools.Domain.Common.Interfaces;
using TherapyTools.Application.TherapyManagement.Commands;
using TherapyTools.Application.TherapyManagement.IntegrationEvents;
using Mediator;

var builder = WebApplication.CreateBuilder(args);

// Add InProcessEventStore to DI
builder.Services.AddSingleton<IEventStore<TherapyPlanId>, InProcessEventStore<TherapyPlanId>>();
builder.Services.AddSingleton<IEventStore<TherapySessionId>, InProcessEventStore<TherapySessionId>>();

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

// Mutation type for all commands
public class Mutation
{
    public async Task<bool> CreateTherapyPlan(
        CreateTherapyPlanCommand command,
        [Service] IMediator mediator)
        => await mediator.Send(command) is not null;

    public async Task<bool> ActivateTherapyPlan(
        ActivateTherapyPlanCommand command,
        [Service] IMediator mediator)
        => await mediator.Send(command) is not null;

    public async Task<bool> CompleteTherapyPlan(
        CompleteTherapyPlanCommand command,
        [Service] IMediator mediator)
        => await mediator.Send(command) is not null;

    public async Task<bool> DiscardTherapyPlan(
        DiscardTherapyPlanCommand command,
        [Service] IMediator mediator)
        => await mediator.Send(command) is not null;

    public async Task<bool> ScheduleTherapySession(
        ScheduleTherapySessionCommand command,
        [Service] IMediator mediator)
        => await mediator.Send(command) is not null;

    public async Task<bool> RescheduleTherapySession(
        RescheduleTherapySessionCommand command,
        [Service] IMediator mediator)
        => await mediator.Send(command) is not null;

    public async Task<bool> CancelTherapySession(
        CancelTherapySessionCommand command,
        [Service] IMediator mediator)
        => await mediator.Send(command) is not null;

    public async Task<bool> CompleteTherapySession(
        CompleteTherapySessionCommand command,
        [Service] IMediator mediator)
        => await mediator.Send(command) is not null;

    public async Task<bool> UpdateTherapySessionNotes(
        UpdateTherapySessionNotesCommand command,
        [Service] IMediator mediator)
        => await mediator.Send(command) is not null;
}

// Subscription type remains unchanged
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

using HotChocolate;
using HotChocolate.AspNetCore;
using HotChocolate.Execution.Configuration;
using Microsoft.Extensions.DependencyInjection;
using TherapyTools.Application.TherapyManagement.Commands;
using TherapyTools.Domain.TherapyManagement;
using HotChocolate.Subscriptions;
using FluentValidation;
using TherapyTools.Application.Common.Interfaces;
using TherapyTools.Domain.Common.Interfaces;
using HotChocolate.Execution;

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
public record TherapyPlanIntegrationEvent(string EventName, TherapyPlanState State);
public record TherapySessionIntegrationEvent(string EventName, TherapySessionState State);

// Placeholder mutation and subscription types
public class Mutation
{
    private async Task<TAggregateState> HandleAggregateCommand<TCommand, TAggregateId, TAggregateState, TIntegrationEvent>(
        TCommand input,
        IAggregateCommandHandler<TCommand, TAggregateId, TAggregateState> handler,
        IValidator<TCommand> validator,
        IEventStore<TAggregateId> eventStore,
        ITopicEventSender eventSender,
        Func<IDomainEvent, TAggregateState, TIntegrationEvent> integrationEventFactory,
        Func<IEventStore<TAggregateId>, TAggregateId, Task<TAggregateState>> getState,
        Func<TAggregateState, IDomainEvent, TAggregateState> aggregateApply,
        string topicPrefix)
        where TCommand : IAggregateCommand<TAggregateId>
        where TAggregateId : IAggregateId
        where TAggregateState : AggregateState<TAggregateId>
    {
        await validator.ValidateAndThrowAsync(input);
        var state = await getState(eventStore, input.AggregateId);
        var events = await handler.Handle(input, state);
        foreach (var @event in events)
        {
            await eventStore.Append(@event);
            state = aggregateApply(state, @event);
            var integrationEvent = integrationEventFactory(@event, state);
            await eventSender.SendAsync($"{topicPrefix}_{input.AggregateId.ToGuid()}", integrationEvent);
        }
        return state;
    }

    // Helper for TherapyPlan mutations
    private Task<TherapyPlanState> HandleTherapyPlanCommand<TCommand>(
        TCommand input,
        IAggregateCommandHandler<TCommand, TherapyPlanId, TherapyPlanState> handler,
        IValidator<TCommand> validator,
        IEventStore<TherapyPlanId> eventStore,
        ITopicEventSender eventSender)
        where TCommand : IAggregateCommand<TherapyPlanId>
        => HandleAggregateCommand(
            input,
            handler,
            validator,
            eventStore,
            eventSender,
            (e, s) => new TherapyPlanIntegrationEvent(e.GetType().ToString(), s),
            TherapyPlanAggregate.GetCurrentState,
            TherapyPlanAggregate.Apply,
            "TherapyPlan");

    // Helper for TherapySession mutations
    private Task<TherapySessionState> HandleTherapySessionCommand<TCommand>(
        TCommand input,
        IAggregateCommandHandler<TCommand, TherapySessionId, TherapySessionState> handler,
        IValidator<TCommand> validator,
        IEventStore<TherapySessionId> eventStore,
        ITopicEventSender eventSender)
        where TCommand : IAggregateCommand<TherapySessionId>
        => HandleAggregateCommand(
            input,
            handler,
            validator,
            eventStore,
            eventSender,
            (e, s) => new TherapySessionIntegrationEvent(e.GetType().ToString(), s),
            TherapySessionAggregate.GetCurrentState,
            TherapySessionAggregate.Apply,
            "TherapySession");

    // TherapyPlan Mutations
    public Task<TherapyPlanState> CreateTherapyPlan(
        CreateTherapyPlanCommand input,
        [Service] IAggregateCommandHandler<CreateTherapyPlanCommand, TherapyPlanId, TherapyPlanState> handler,
        [Service] IValidator<CreateTherapyPlanCommand> validator,
        [Service] IEventStore<TherapyPlanId> eventStore,
        [Service] ITopicEventSender eventSender) =>
        HandleTherapyPlanCommand(input, handler, validator, eventStore, eventSender);

    public Task<TherapyPlanState> ActivateTherapyPlan(
        ActivateTherapyPlanCommand input,
        [Service] IAggregateCommandHandler<ActivateTherapyPlanCommand, TherapyPlanId, TherapyPlanState> handler,
        [Service] IValidator<ActivateTherapyPlanCommand> validator,
        [Service] IEventStore<TherapyPlanId> eventStore,
        [Service] ITopicEventSender eventSender) =>
        HandleTherapyPlanCommand(input, handler, validator, eventStore, eventSender);

    public Task<TherapyPlanState> CompleteTherapyPlan(
        CompleteTherapyPlanCommand input,
        [Service] IAggregateCommandHandler<CompleteTherapyPlanCommand, TherapyPlanId, TherapyPlanState> handler,
        [Service] IValidator<CompleteTherapyPlanCommand> validator,
        [Service] IEventStore<TherapyPlanId> eventStore,
        [Service] ITopicEventSender eventSender) =>
        HandleTherapyPlanCommand(input, handler, validator, eventStore, eventSender);

    public Task<TherapyPlanState> DiscardTherapyPlan(
        DiscardTherapyPlanCommand input,
        [Service] IAggregateCommandHandler<DiscardTherapyPlanCommand, TherapyPlanId, TherapyPlanState> handler,
        [Service] IValidator<DiscardTherapyPlanCommand> validator,
        [Service] IEventStore<TherapyPlanId> eventStore,
        [Service] ITopicEventSender eventSender) =>
        HandleTherapyPlanCommand(input, handler, validator, eventStore, eventSender);

    // TherapySession Mutations
    public Task<TherapySessionState> ScheduleTherapySession(
        ScheduleTherapySessionCommand input,
        [Service] IAggregateCommandHandler<ScheduleTherapySessionCommand, TherapySessionId, TherapySessionState> handler,
        [Service] IValidator<ScheduleTherapySessionCommand> validator,
        [Service] IEventStore<TherapySessionId> eventStore,
        [Service] ITopicEventSender eventSender) =>
        HandleTherapySessionCommand(input, handler, validator, eventStore, eventSender);

    public Task<TherapySessionState> RescheduleTherapySession(
        RescheduleTherapySessionCommand input,
        [Service] IAggregateCommandHandler<RescheduleTherapySessionCommand, TherapySessionId, TherapySessionState> handler,
        [Service] IValidator<RescheduleTherapySessionCommand> validator,
        [Service] IEventStore<TherapySessionId> eventStore,
        [Service] ITopicEventSender eventSender) =>
        HandleTherapySessionCommand(input, handler, validator, eventStore, eventSender);

    public Task<TherapySessionState> CancelTherapySession(
        CancelTherapySessionCommand input,
        [Service] IAggregateCommandHandler<CancelTherapySessionCommand, TherapySessionId, TherapySessionState> handler,
        [Service] IValidator<CancelTherapySessionCommand> validator,
        [Service] IEventStore<TherapySessionId> eventStore,
        [Service] ITopicEventSender eventSender) =>
        HandleTherapySessionCommand(input, handler, validator, eventStore, eventSender);

    public Task<TherapySessionState> CompleteTherapySession(
        CompleteTherapySessionCommand input,
        [Service] IAggregateCommandHandler<CompleteTherapySessionCommand, TherapySessionId, TherapySessionState> handler,
        [Service] IValidator<CompleteTherapySessionCommand> validator,
        [Service] IEventStore<TherapySessionId> eventStore,
        [Service] ITopicEventSender eventSender) =>
        HandleTherapySessionCommand(input, handler, validator, eventStore, eventSender);

    public Task<TherapySessionState> UpdateTherapySessionNotes(
        UpdateTherapySessionNotesCommand input,
        [Service] IAggregateCommandHandler<UpdateTherapySessionNotesCommand, TherapySessionId, TherapySessionState> handler,
        [Service] IValidator<UpdateTherapySessionNotesCommand> validator,
        [Service] IEventStore<TherapySessionId> eventStore,
        [Service] ITopicEventSender eventSender) =>
        HandleTherapySessionCommand(input, handler, validator, eventStore, eventSender);
}

public class Subscription
{
    [Subscribe]
    [Topic($"TherapyPlan_{{{nameof(id)}}}")]
    public TherapyPlanIntegrationEvent OnTherapyPlanChanged(TherapyPlanId id, [EventMessage] TherapyPlanIntegrationEvent @event)
        => @event;

    [Subscribe]
    [Topic($"TherapySession_{{{nameof(id)}}}")]
    public TherapySessionIntegrationEvent OnTherapySessionChanged(TherapyPlanId id, [EventMessage] TherapySessionIntegrationEvent @event)
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

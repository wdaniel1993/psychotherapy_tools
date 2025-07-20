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
    .AddQueryType(d => d.Name("Query")) // Placeholder for queries
    .AddMutationType<Mutation>()
    .AddSubscriptionType<Subscription>()
    .AddInMemorySubscriptions();

var app = builder.Build();

app.UseRouting();
app.UseWebSockets();
app.MapGraphQL();

app.Run();

// Integration event types for GraphQL subscriptions
public record TherapyPlanIntegrationEvent(object DomainEvent, TherapyTools.Domain.TherapyManagement.TherapyPlanState State);
public record TherapySessionIntegrationEvent(object DomainEvent, TherapyTools.Domain.TherapyManagement.TherapySessionState);

// Placeholder mutation and subscription types
public class Mutation
{
    // Generic mutation handler
    private async Task<TIntegrationEvent> HandleCommand<TCommand, TAggregateId, TAggregateState, TIntegrationEvent>(
        TCommand input,
        IAggregateCommandHandler<TCommand, TAggregateId, TAggregateState> handler,
        IValidator<TCommand> validator,
        IEventStore<TAggregateId> eventStore,
        ITopicEventSender eventSender,
        Func<object, TAggregateState, TIntegrationEvent> integrationEventFactory,
        Func<IEventStore<TAggregateId>, TAggregateId, Task<TAggregateState>> getState,
        string topicPrefix)
        where TCommand : IAggregateCommand<TAggregateId>
        where TAggregateId : IAggregateId
        where TAggregateState : AggregateState<TAggregateId>
    {
        await validator.ValidateAndThrowAsync(input);
        var id = (TAggregateId)typeof(TCommand).GetProperty("Id")!.GetValue(input)!;
        var state = await getState(eventStore, id);
        var events = await handler.Handle(input, state);
        await eventStore.Append(events);
        var newState = await getState(eventStore, id);
        var integrationEvent = integrationEventFactory(events.First(), newState);
        await eventSender.SendAsync($"{topicPrefix}_{id}", integrationEvent);
        return integrationEvent;
    }

    // TherapyPlan Mutations
    public Task<TherapyPlanIntegrationEvent> CreateTherapyPlan(
        CreateTherapyPlanCommand input,
        [Service] IAggregateCommandHandler<CreateTherapyPlanCommand, TherapyPlanId, TherapyPlanState> handler,
        [Service] IValidator<CreateTherapyPlanCommand> validator,
        [Service] IEventStore<TherapyPlanId> eventStore,
        [Service] ITopicEventSender eventSender) =>
        HandleCommand(input, handler, validator, eventStore, eventSender,
            (e, s) => new TherapyPlanIntegrationEvent(e, s),
            TherapyPlanAggregate.GetCurrentState, "TherapyPlan");

    public Task<TherapyPlanIntegrationEvent> ActivateTherapyPlan(
        ActivateTherapyPlanCommand input,
        [Service] IAggregateCommandHandler<ActivateTherapyPlanCommand, TherapyPlanId, TherapyPlanState> handler,
        [Service] IValidator<ActivateTherapyPlanCommand> validator,
        [Service] IEventStore<TherapyPlanId> eventStore,
        [Service] ITopicEventSender eventSender) =>
        HandleCommand(input, handler, validator, eventStore, eventSender,
            (e, s) => new TherapyPlanIntegrationEvent(e, s),
            TherapyPlanAggregate.GetCurrentState, "TherapyPlan");

    public Task<TherapyPlanIntegrationEvent> CompleteTherapyPlan(
        CompleteTherapyPlanCommand input,
        [Service] IAggregateCommandHandler<CompleteTherapyPlanCommand, TherapyPlanId, TherapyPlanState> handler,
        [Service] IValidator<CompleteTherapyPlanCommand> validator,
        [Service] IEventStore<TherapyPlanId> eventStore,
        [Service] ITopicEventSender eventSender) =>
        HandleCommand(input, handler, validator, eventStore, eventSender,
            (e, s) => new TherapyPlanIntegrationEvent(e, s),
            TherapyPlanAggregate.GetCurrentState, "TherapyPlan");

    public Task<TherapyPlanIntegrationEvent> DiscardTherapyPlan(
        DiscardTherapyPlanCommand input,
        [Service] IAggregateCommandHandler<DiscardTherapyPlanCommand, TherapyPlanId, TherapyPlanState> handler,
        [Service] IValidator<DiscardTherapyPlanCommand> validator,
        [Service] IEventStore<TherapyPlanId> eventStore,
        [Service] ITopicEventSender eventSender) =>
        HandleCommand(input, handler, validator, eventStore, eventSender,
            (e, s) => new TherapyPlanIntegrationEvent(e, s),
            TherapyPlanAggregate.GetCurrentState, "TherapyPlan");

    // TherapySession Mutations
    public Task<TherapySessionIntegrationEvent> ScheduleTherapySession(
        ScheduleTherapySessionCommand input,
        [Service] IAggregateCommandHandler<ScheduleTherapySessionCommand, TherapySessionId, TherapySessionState> handler,
        [Service] IValidator<ScheduleTherapySessionCommand> validator,
        [Service] IEventStore<TherapySessionId> eventStore,
        [Service] ITopicEventSender eventSender) =>
        HandleCommand(input, handler, validator, eventStore, eventSender,
            (e, s) => new TherapySessionIntegrationEvent(e, s),
            TherapySessionAggregate.GetCurrentState, "TherapySession");

    public Task<TherapySessionIntegrationEvent> RescheduleTherapySession(
        RescheduleTherapySessionCommand input,
        [Service] IAggregateCommandHandler<RescheduleTherapySessionCommand, TherapySessionId, TherapySessionState> handler,
        [Service] IValidator<RescheduleTherapySessionCommand> validator,
        [Service] IEventStore<TherapySessionId> eventStore,
        [Service] ITopicEventSender eventSender) =>
        HandleCommand(input, handler, validator, eventStore, eventSender,
            (e, s) => new TherapySessionIntegrationEvent(e, s),
            TherapySessionAggregate.GetCurrentState, "TherapySession");

    public Task<TherapySessionIntegrationEvent> CancelTherapySession(
        CancelTherapySessionCommand input,
        [Service] IAggregateCommandHandler<CancelTherapySessionCommand, TherapySessionId, TherapySessionState> handler,
        [Service] IValidator<CancelTherapySessionCommand> validator,
        [Service] IEventStore<TherapySessionId> eventStore,
        [Service] ITopicEventSender eventSender) =>
        HandleCommand(input, handler, validator, eventStore, eventSender,
            (e, s) => new TherapySessionIntegrationEvent(e, s),
            TherapySessionAggregate.GetCurrentState, "TherapySession");

    public Task<TherapySessionIntegrationEvent> CompleteTherapySession(
        CompleteTherapySessionCommand input,
        [Service] IAggregateCommandHandler<CompleteTherapySessionCommand, TherapySessionId, TherapySessionState> handler,
        [Service] IValidator<CompleteTherapySessionCommand> validator,
        [Service] IEventStore<TherapySessionId> eventStore,
        [Service] ITopicEventSender eventSender) =>
        HandleCommand(input, handler, validator, eventStore, eventSender,
            (e, s) => new TherapySessionIntegrationEvent(e, s),
            TherapySessionAggregate.GetCurrentState, "TherapySession");

    public Task<TherapySessionIntegrationEvent> UpdateTherapySessionNotes(
        UpdateTherapySessionNotesCommand input,
        [Service] IAggregateCommandHandler<UpdateTherapySessionNotesCommand, TherapySessionId, TherapySessionState> handler,
        [Service] IValidator<UpdateTherapySessionNotesCommand> validator,
        [Service] IEventStore<TherapySessionId> eventStore,
        [Service] ITopicEventSender eventSender) =>
        HandleCommand(input, handler, validator, eventStore, eventSender,
            (e, s) => new TherapySessionIntegrationEvent(e, s),
            TherapySessionAggregate.GetCurrentState, "TherapySession");
}

public class Subscription
{
    [Subscribe]
    public async ValueTask<ISourceStream<TherapyPlanIntegrationEvent>> OnTherapyPlanChanged(
        TherapyPlanId id,
        [Service] ITopicEventReceiver eventReceiver)
    {
        return await eventReceiver.SubscribeAsync<TherapyPlanIntegrationEvent>($"TherapyPlan_{id}");
    }
    [Subscribe]
    public async ValueTask<ISourceStream<TherapySessionIntegrationEvent>> OnTherapySessionChanged(
        TherapySessionId id,
        [Service] ITopicEventReceiver eventReceiver)
    {
        return await eventReceiver.SubscribeAsync<TherapySessionIntegrationEvent>($"TherapySession_{id}");
    }
}

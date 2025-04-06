using TherapyTools.Domain.Common.Cqrs;
using TherapyTools.Domain.TherapyManagement.ValueObjects;

namespace TherapyTools.Domain.TherapyManagement;

public enum TherapyPlanStatus
{
    Draft,
    Active,
    Completed,
    Discarded
}

public record TherapyPlanState(
    IReadOnlyList<Goal> Goals,
    string Description,
    TherapyPlanStatus Status
);

public static class TherapyPlanAggregate
{
    public static TherapyPlanState Apply(TherapyPlanState state, IDomainEvent @event) =>
        @event switch
        {
            TherapyPlanCreated e => state with
            {
                Goals = e.Goals,
                Description = e.Description,
                Status = TherapyPlanStatus.Draft
            },
            TherapyPlanActivated e => state with { Status = TherapyPlanStatus.Active },
            TherapyPlanCompleted e => state with { Status = TherapyPlanStatus.Completed },
            TherapyPlanDiscard e => state with { Status = TherapyPlanStatus.Discarded },
            _ => state
        };

    public static TherapyPlanState InitialState() =>
        new(
            [],
            string.Empty,
            TherapyPlanStatus.Draft
        );

    public static TherapyPlanState Replay(IEnumerable<IDomainEvent> events)
        => events.Aggregate(InitialState(), Apply);

    public static async Task<TherapyPlanState> GetCurrentState(IEventStore eventStore, Guid id)
        => Replay(await eventStore.GetEvents(id));
}

public record TherapyPlanCreated(Guid Id, IReadOnlyList<Goal> Goals, string Description) : IDomainEvent;
public record TherapyPlanActivated(Guid Id) : IDomainEvent;
public record TherapyPlanCompleted(Guid Id) : IDomainEvent;
public record TherapyPlanDiscard(Guid Id) : IDomainEvent;

public record CreateTherapyPlanCommand(Guid Id, IReadOnlyList<Goal> Goals, string Description) : IDomainCommand;
public record ActivateTherapyPlanCommand(Guid Id) : IDomainCommand;
public record CompleteTherapyPlanCommand(Guid Id) : IDomainCommand;
public record DiscardTherapyPlanCommand(Guid Id) : IDomainCommand;
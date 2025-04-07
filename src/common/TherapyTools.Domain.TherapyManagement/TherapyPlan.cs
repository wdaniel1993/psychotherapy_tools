using TherapyTools.Domain.Common.Cqrs;

namespace TherapyTools.Domain.TherapyManagement;

public enum TherapyPlanStatus
{
    Draft,
    Active,
    Completed,
    Discarded
}

public readonly record struct TherapyPlanId(Guid Id) : IConvertTo<Guid>
{
    public static TherapyPlanId New() => new(Guid.NewGuid());
    public static TherapyPlanId From(Guid id) => new(id);
    public readonly Guid To() => Id;
}

public readonly record struct TherapyPlanDescription(string Description);
public readonly record struct Goal (string Name, string Description);
public readonly record struct GoalList(IReadOnlyList<Goal> Goals) {
    public static GoalList Empty => new([]);
};

public record TherapyPlanState(
    GoalList GoalList,
    TherapyPlanDescription Description,
    TherapyPlanStatus Status
);

public static class TherapyPlanAggregate
{
    public static TherapyPlanState Apply(TherapyPlanState state, IDomainEvent @event) =>
        @event switch
        {
            TherapyPlanCreated e => state with
            {
                GoalList = e.GoalList,
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
            GoalList.Empty,
            new TherapyPlanDescription(string.Empty),
            TherapyPlanStatus.Draft
        );

    public static TherapyPlanState Replay(IEnumerable<IDomainEvent> events)
        => events.Aggregate(InitialState(), Apply);

    public static async Task<TherapyPlanState> GetCurrentState(IEventStore<TherapyPlanId> eventStore, TherapyPlanId id)
        => Replay(await eventStore.GetEvents(id));
}

public record TherapyPlanCreated(TherapyPlanId Id, GoalList GoalList, TherapyPlanDescription Description) : IDomainEvent;
public record TherapyPlanActivated(TherapyPlanId Id) : IDomainEvent;
public record TherapyPlanCompleted(TherapyPlanId Id) : IDomainEvent;
public record TherapyPlanDiscard(TherapyPlanId Id) : IDomainEvent;

public record CreateTherapyPlanCommand(TherapyPlanId Id, GoalList GoalList, TherapyPlanDescription Description) : IDomainCommand;
public record ActivateTherapyPlanCommand(TherapyPlanId Id) : IDomainCommand;
public record CompleteTherapyPlanCommand(TherapyPlanId Id) : IDomainCommand;
public record DiscardTherapyPlanCommand(TherapyPlanId Id) : IDomainCommand;
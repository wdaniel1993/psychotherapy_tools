using TherapyTools.Domain.Common.Cqrs;

namespace TherapyTools.Domain.TherapyManagement;


public enum TherapyPlanStatus
{
    Draft,
    Active,
    Completed,
    Discarded
}

public readonly record struct TherapyPlanId(Guid Id) : IAggregateId
{
    public static TherapyPlanId New() => new(Guid.NewGuid());
    public static TherapyPlanId From(Guid id) => new(id);
    public readonly Guid ToGuid() => Id;
}

public readonly record struct TherapyPlanDescription(string Description);
public readonly record struct Goal (string Name, string Description);
public readonly record struct GoalList(IReadOnlyList<Goal> Goals) {
    public static GoalList Empty => new([]);
};

public record TherapyPlanState(
    TherapyPlanId Id,
    GoalList GoalList,
    TherapyPlanDescription Description,
    TherapyPlanStatus Status
) : AggregateState<TherapyPlanId>(Id);

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
            TherapyPlanId.New(),
            GoalList.Empty,
            new TherapyPlanDescription(string.Empty),
            TherapyPlanStatus.Draft
        );

    public static TherapyPlanState Replay(IEnumerable<IDomainEvent> events)
        => events.Aggregate(InitialState(), Apply);

    public static async Task<TherapyPlanState> GetCurrentState(IEventStore<TherapyPlanId> eventStore, TherapyPlanId id)
        => Replay(await eventStore.GetEvents(id));
}

public record TherapyPlanCreated(TherapyPlanId Id, GoalList GoalList, TherapyPlanDescription Description) : AggregateDomainEvent<TherapyPlanId>(Id);
public record TherapyPlanActivated(TherapyPlanId Id) : AggregateDomainEvent<TherapyPlanId>(Id);
public record TherapyPlanCompleted(TherapyPlanId Id) : AggregateDomainEvent<TherapyPlanId>(Id);
public record TherapyPlanDiscard(TherapyPlanId Id) : AggregateDomainEvent<TherapyPlanId>(Id);

public record CreateTherapyPlanCommand(TherapyPlanId Id, GoalList GoalList, TherapyPlanDescription Description) : AggregateCommand<TherapyPlanId>(Id);
public record ActivateTherapyPlanCommand(TherapyPlanId Id) : AggregateCommand<TherapyPlanId>(Id);
public record CompleteTherapyPlanCommand(TherapyPlanId Id) : AggregateCommand<TherapyPlanId>(Id);
public record DiscardTherapyPlanCommand(TherapyPlanId Id) : AggregateCommand<TherapyPlanId>(Id);
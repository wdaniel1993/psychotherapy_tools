using TherapyTools.Domain.Common;
using TherapyTools.Domain.Common.Interfaces;

namespace TherapyTools.Domain.TherapyManagement;


public enum TherapyPlanStatus
{
    Draft,
    Active,
    Completed,
    Discarded
}

public readonly record struct TherapyPlanId(Guid Value) : IAggregateId
{
    public static TherapyPlanId New() => new(Guid.NewGuid());
    public static TherapyPlanId From(Guid id) => new(id);
    public Guid ToGuid() => Value;
}

public readonly record struct TherapyPlanDescription(string Content);
public readonly record struct Goal (string Title, string Description);
public readonly record struct GoalList(IReadOnlyList<Goal> Goals) {
    public static GoalList Empty => new([]);
};

public record TherapyPlanState(
    TherapyPlanId AggregateId,
    GoalList GoalList,
    TherapyPlanDescription Description,
    TherapyPlanStatus Status
) : AggregateState<TherapyPlanId>(AggregateId);

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
            TherapyPlanActivated _ => state with { Status = TherapyPlanStatus.Active },
            TherapyPlanCompleted _ => state with { Status = TherapyPlanStatus.Completed },
            TherapyPlanDiscarded _ => state with { Status = TherapyPlanStatus.Discarded },
            _ => state
        };

    public static TherapyPlanState Apply(TherapyPlanState state, IEnumerable<IDomainEvent> events)
        => events.Aggregate(state, Apply);


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
public record TherapyPlanDiscarded(TherapyPlanId Id) : AggregateDomainEvent<TherapyPlanId>(Id);
using TherapyTools.Domain.Common;
using TherapyTools.Domain.Common.Interfaces;
using TherapyTools.Domain.TherapyManagement.ValueObjects;

namespace TherapyTools.Domain.TherapyManagement;

public enum TherapySessionStatus
{
    Unconfirmed,
    Scheduled,
    Canceled,
    Done
}

public enum TherapySessionType
{
    Individual,
    Group,
    Family
}

public readonly record struct TherapySessionId(Guid Id) : IAggregateId
{
    public static TherapySessionId New() => new(Guid.NewGuid());
    public static TherapySessionId From(Guid id) => new(id);
    public Guid ToGuid() => Id;
}

public readonly record struct SessionNotes (string Content);

public record TherapySessionState(
    TherapySessionId AggregateId,
    TimeSlot SessionTimeSlot,
    TherapySessionType Type,
    SessionNotes Notes,
    TherapySessionStatus Status
) :  AggregateState<TherapySessionId>(AggregateId);

public static class TherapySessionAggregate
{
    public static TherapySessionState Apply(TherapySessionState state, IDomainEvent @event) =>
        @event switch
        {
            TherapySessionScheduled e => state with
            {
                SessionTimeSlot = e.SessionTimeSlot,
                Type = e.Type,
                Notes = e.Notes,
                Status = TherapySessionStatus.Scheduled
            },
            TherapySessionRescheduled e => state with { SessionTimeSlot = e.NewSlot },
            TherapySessionCanceled _ => state with { Status = TherapySessionStatus.Canceled },
            TherapySessionCompleted e => state with
            {
                Notes = e.Notes,
                Status = TherapySessionStatus.Done
            },
            TherapySessionNotesUpdates e => state with { Notes = e.Notes },
            _ => state
        };

    public static TherapySessionState Apply(TherapySessionState state, IEnumerable<IDomainEvent> events)
        => events.Aggregate(state, Apply);

    public static TherapySessionState InitialState() => 
        new(
            TherapySessionId.New(),
            new TimeSlot(DateTime.MinValue, DateTime.MaxValue),
            TherapySessionType.Individual,
            new SessionNotes(string.Empty),
            TherapySessionStatus.Unconfirmed
        );

    public static TherapySessionState Replay(IEnumerable<IDomainEvent> events)
        => events.Aggregate(InitialState(), Apply);

    public static async Task<TherapySessionState> GetCurrentState(IEventStore<TherapySessionId> eventStore, TherapySessionId id)
        => Replay(await eventStore.GetEvents(id));
}

public record TherapySessionScheduled(TherapySessionId Id, TimeSlot SessionTimeSlot, TherapySessionType Type, SessionNotes Notes) : AggregateDomainEvent<TherapySessionId>(Id);
public record TherapySessionRescheduled(TherapySessionId Id, TimeSlot NewSlot) : AggregateDomainEvent<TherapySessionId>(Id);
public record TherapySessionCompleted(TherapySessionId Id, SessionNotes Notes) : AggregateDomainEvent<TherapySessionId>(Id);
public record TherapySessionNotesUpdates(TherapySessionId Id, SessionNotes Notes) : AggregateDomainEvent<TherapySessionId>(Id);
public record TherapySessionCanceled(TherapySessionId Id) : AggregateDomainEvent<TherapySessionId>(Id);
using TherapyTools.Domain.Common.Cqrs;
using TherapyTools.Domain.TherapyManagement.Shared;

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

public readonly record struct SessionNotes (string Notes);

public record TherapySessionState(
    TimeSlot SessionTimeSlot,
    TherapySessionType Type,
    SessionNotes Notes,
    TherapySessionStatus Status
);

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
            TherapySessionCanceled e => state with { Status = TherapySessionStatus.Canceled },
            TherapySessionCompleted e => state with
            {
                Notes = e.Notes,
                Status = TherapySessionStatus.Done
            },
            TherapySessionNotesUpdates e => state with { Notes = e.Notes },
            _ => state
        };

    public static TherapySessionState InitialState() => 
        new(
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

public record TherapySessionScheduled(TherapySessionId Id, TimeSlot SessionTimeSlot, TherapySessionType Type, SessionNotes Notes) : IDomainEvent;
public record TherapySessionRescheduled(TherapySessionId Id, TimeSlot NewSlot) : IDomainEvent;
public record TherapySessionCompleted(TherapySessionId Id, SessionNotes Notes) : IDomainEvent;
public record TherapySessionNotesUpdates(TherapySessionId Id, SessionNotes Notes) : IDomainEvent;
public record TherapySessionCanceled(TherapySessionId Id) : IDomainEvent;

public record ScheduleTherapySessionCommand(TherapySessionId Id, TimeSlot SessionTimeSlot, TherapySessionType Type, SessionNotes Notes) : AggregateCommand<TherapySessionId>(Id);
public record RescheduleTherapySessionCommand(TherapySessionId Id, TimeSlot NewSlot) : AggregateCommand<TherapySessionId>(Id);
public record CancelTherapySessionCommand(TherapySessionId Id) : AggregateCommand<TherapySessionId>(Id);
public record CompleteTherapySessionCommand(TherapySessionId Id, SessionNotes Notes) : AggregateCommand<TherapySessionId>(Id);
public record UpdateTherapySessionNotesCommand(TherapySessionId Id, SessionNotes Notes) : AggregateCommand<TherapySessionId>(Id);
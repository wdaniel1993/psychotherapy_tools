using TherapyTools.Domain.Common.Cqrs;
using TherapyTools.Domain.TherapyManagement.ValueObjects;

namespace TherapyTools.Domain.TherapyManagement;

public interface ITherapySessionCommand : IDomainCommand { }

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

    public static async Task<TherapySessionState> GetCurrentState(IEventStore eventStore, Guid id)
        => Replay(await eventStore.GetEvents(id));
}

public record TherapySessionScheduled(Guid Id, TimeSlot SessionTimeSlot, TherapySessionType Type, SessionNotes Notes) : IDomainEvent;
public record TherapySessionRescheduled(Guid Id, TimeSlot NewSlot) : IDomainEvent;
public record TherapySessionCompleted(Guid Id, SessionNotes Notes) : IDomainEvent;
public record TherapySessionNotesUpdates(Guid Id, SessionNotes Notes) : IDomainEvent;
public record TherapySessionCanceled(Guid Id) : IDomainEvent;

public record ScheduleTherapySessionCommand(Guid Id, TimeSlot SessionTimeSlot, TherapySessionType Type, SessionNotes Notes) : ITherapySessionCommand;
public record RescheduleTherapySessionCommand(Guid Id, TimeSlot NewSlot) : ITherapySessionCommand;
public record CancelTherapySessionCommand(Guid Id) : ITherapySessionCommand;
public record CompleteTherapySessionCommand(Guid Id, SessionNotes Notes) : ITherapySessionCommand;
public record UpdateTherapySessionNotesCommand(Guid Id, SessionNotes Notes) : ITherapySessionCommand;
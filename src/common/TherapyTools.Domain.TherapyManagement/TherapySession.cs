using TherapyTools.Domain.Common.Cqrs;

namespace TherapyTools.Domain.TherapyManagement;

public enum TherapySessionStatus
{
    Scheduled,
    Canceled
}

public enum TherapySessionType
{
    Individual,
    Group,
    Family
}

public record TherapySession
{
    public Guid Id { get; init; }
    public DateTime SessionDate { get; init; }
    public required TherapySessionType Type { get; init; }
    public TimeSpan Duration { get; init; }
    public required string Notes { get; init; }
    public required TherapySessionStatus Status { get; init; }

    public static TherapySession Schedule(Guid id, DateTime sessionDate, TherapySessionType type, TimeSpan duration, string notes) =>
        new TherapySession
        {
            Id = id,
            SessionDate = sessionDate,
            Type = type,
            Duration = duration,
            Notes = notes,
            Status = TherapySessionStatus.Scheduled
        };

    public TherapySession Reschedule(DateTime newDate) => this with { SessionDate = newDate };

    public TherapySession Cancel() => this with { Status = TherapySessionStatus.Canceled };
}

public record TherapySessionScheduled(Guid Id, DateTime SessionDate, TherapySessionType Type, TimeSpan Duration, string Notes) : IDomainEvent;
public record TherapySessionRescheduled(Guid Id, DateTime NewDate) : IDomainEvent;
public record TherapySessionCanceled(Guid Id) : IDomainEvent;

public record ScheduleTherapySessionCommand(Guid Id, DateTime SessionDate, TherapySessionType Type, TimeSpan Duration, string Notes);
public record RescheduleTherapySessionCommand(Guid Id, DateTime NewDate);
public record CancelTherapySessionCommand(Guid Id);

public class TherapySessionCommandHandler
{
    public static IEnumerable<IDomainEvent> Handle(ScheduleTherapySessionCommand command)
    {
        yield return new TherapySessionScheduled(command.Id, command.SessionDate, command.Type, command.Duration, command.Notes);
    }

    public static IEnumerable<IDomainEvent> Handle(RescheduleTherapySessionCommand command)
    {
        yield return new TherapySessionRescheduled(command.Id, command.NewDate);
    }

    public static IEnumerable<IDomainEvent> Handle(CancelTherapySessionCommand command)
    {
        yield return new TherapySessionCanceled(command.Id);
    }
}

public class TherapySessionEventHandler
{
    public static TherapySession Apply(TherapySessionScheduled @event) =>
        TherapySession.Schedule(@event.Id, @event.SessionDate, @event.Type, @event.Duration, @event.Notes);

    public static TherapySession Apply(TherapySessionRescheduled @event, TherapySession session) =>
        session.Reschedule(@event.NewDate);

    public static TherapySession Apply(TherapySessionCanceled @event, TherapySession session) =>
        session.Cancel();
}
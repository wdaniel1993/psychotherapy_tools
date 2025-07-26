using TherapyTools.Application.Common.Interfaces;
using TherapyTools.Domain.TherapyManagement;

public record TherapySessionScheduledEventModel(
    Guid AggregateId,
    TimeSlotModel SessionTimeSlot,
    TherapySessionType Type,
    string Notes
) : IAggregateEventModel;

public static class TherapySessionScheduledEventModelMapper
{
    public static TherapySessionScheduledEventModel ToModel(this TherapySessionScheduled domain)
        => new(
            domain.Id.ToGuid(),
            domain.SessionTimeSlot.ToModel(),
            domain.Type,
            domain.Notes.Content
        );

    public static TherapySessionScheduled ToDomain(this TherapySessionScheduledEventModel model)
        => new(
            new TherapySessionId(model.AggregateId),
            model.SessionTimeSlot.ToDomain(),
            model.Type,
            new SessionNotes(model.Notes)
        );
}

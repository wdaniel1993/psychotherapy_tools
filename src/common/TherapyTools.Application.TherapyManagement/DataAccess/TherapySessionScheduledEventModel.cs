using System;
using TherapyTools.Application.TherapyManagement.Interfaces;
using TherapyTools.Domain.TherapyManagement;
using TherapyTools.Domain.TherapyManagement.ValueObjects;

public record TherapySessionScheduledEventModel(
    Guid Id,
    TimeSlotModel SessionTimeSlot,
    TherapySessionType Type,
    string Notes
) : ITherapySessionEventModel;

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
            new TherapySessionId(model.Id),
            model.SessionTimeSlot.ToDomain(),
            model.Type,
            new SessionNotes(model.Notes)
        );
}

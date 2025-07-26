using System;
using TherapyTools.Application.TherapyManagement.Interfaces;
using TherapyTools.Domain.TherapyManagement;
using TherapyTools.Domain.TherapyManagement.ValueObjects;

public record TherapySessionRescheduledEventModel(
    Guid Id,
    TimeSlotModel NewSlot
) : ITherapySessionEventModel;

public static class TherapySessionRescheduledEventModelMapper
{
    public static TherapySessionRescheduledEventModel ToModel(this TherapySessionRescheduled domain)
        => new(
            domain.Id.ToGuid(),
            domain.NewSlot.ToModel()
        );

    public static TherapySessionRescheduled ToDomain(this TherapySessionRescheduledEventModel model)
        => new(
            new TherapySessionId(model.Id),
            model.NewSlot.ToDomain()
        );
}

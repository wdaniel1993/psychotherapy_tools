using TherapyTools.Application.Common.Interfaces;
using TherapyTools.Domain.TherapyManagement;

public record TherapySessionRescheduledEventModel(
    Guid AggregateId,
    TimeSlotModel NewSlot
) : IAggregateEventModel;

public static class TherapySessionRescheduledEventModelMapper
{
    public static TherapySessionRescheduledEventModel ToModel(this TherapySessionRescheduled domain)
        => new(
            domain.Id.ToGuid(),
            domain.NewSlot.ToModel()
        );

    public static TherapySessionRescheduled ToDomain(this TherapySessionRescheduledEventModel model)
        => new(
            new TherapySessionId(model.AggregateId),
            model.NewSlot.ToDomain()
        );
}

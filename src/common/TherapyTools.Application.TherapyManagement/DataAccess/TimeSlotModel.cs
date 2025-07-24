using System;
using TherapyTools.Domain.TherapyManagement.ValueObjects;

public record TimeSlotModel(
    DateTime Start,
    DateTime End
);

public static class TimeSlotModelMapper
{
    public static TimeSlotModel ToModel(this TimeSlot slot)
        => new(slot.Start, slot.End);

    public static TimeSlot ToDomain(this TimeSlotModel model)
        => new(model.Start, model.End);
}

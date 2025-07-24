using System;
using TherapyTools.Domain.TherapyManagement;

public record TherapySessionModel(
    Guid AggregateId,
    TimeSlotModel SessionTimeSlot,
    TherapySessionType Type,
    string Notes,
    TherapySessionStatus Status
);

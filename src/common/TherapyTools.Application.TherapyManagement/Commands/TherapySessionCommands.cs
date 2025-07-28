using TherapyTools.Application.Common;
using TherapyTools.Application.TherapyManagement.DataAccess;
using TherapyTools.Domain.TherapyManagement;
using TherapyTools.Domain.TherapyManagement.ValueObjects;

namespace TherapyTools.Application.TherapyManagement.Commands;

public record ScheduleTherapySessionCommand(Guid Id, TimeSlotModel SessionTimeSlot, TherapySessionType Type, string Notes) : AggregateCommand(Id);
public record RescheduleTherapySessionCommand(Guid Id, TimeSlotModel NewSlot) : AggregateCommand(Id);
public record CancelTherapySessionCommand(Guid Id) : AggregateCommand(Id);
public record CompleteTherapySessionCommand(Guid Id, string Notes) : AggregateCommand(Id);
public record UpdateTherapySessionNotesCommand(Guid Id, string Notes) : AggregateCommand(Id);
using TherapyTools.Application.Common;
using TherapyTools.Application.Common.Interfaces;
using TherapyTools.Domain.TherapyManagement;
using TherapyTools.Domain.TherapyManagement.ValueObjects;

namespace TherapyTools.Application.TherapyManagement.Commands;

public record ScheduleTherapySessionCommand(TherapySessionId Id, TimeSlot SessionTimeSlot, TherapySessionType Type, SessionNotes Notes) : AggregateCommand<TherapySessionId>(Id);
public record RescheduleTherapySessionCommand(TherapySessionId Id, TimeSlot NewSlot) : AggregateCommand<TherapySessionId>(Id);
public record CancelTherapySessionCommand(TherapySessionId Id) : AggregateCommand<TherapySessionId>(Id);
public record CompleteTherapySessionCommand(TherapySessionId Id, SessionNotes Notes) : AggregateCommand<TherapySessionId>(Id);
public record UpdateTherapySessionNotesCommand(TherapySessionId Id, SessionNotes Notes) : AggregateCommand<TherapySessionId>(Id);
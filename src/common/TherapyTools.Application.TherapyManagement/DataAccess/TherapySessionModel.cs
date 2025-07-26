using TherapyTools.Application.Common.Interfaces;
using TherapyTools.Domain.TherapyManagement;

public record TherapySessionModel(
    Guid AggregateId,
    TimeSlotModel SessionTimeSlot,
    TherapySessionType Type,
    string Notes,
    TherapySessionStatus Status
) : IAggregateModel ;

public static class TherapySessionModelMapper
{
    public static TherapySessionModel ToModel(this TherapySessionState state)
        => new(
            state.AggregateId.ToGuid(),
            state.SessionTimeSlot.ToModel(),
            state.Type,
            state.Notes.Content,
            state.Status
        );

    public static TherapySessionState ToDomain(this TherapySessionModel model)
        => new(
            new TherapySessionId(model.AggregateId),
            model.SessionTimeSlot.ToDomain(),
            model.Type,
            new SessionNotes(model.Notes),
            model.Status
        );
}

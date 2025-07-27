using TherapyTools.Application.Common.Interfaces;
using TherapyTools.Domain.TherapyManagement;

namespace TherapyTools.Application.TherapyManagement.DataAccess;

public record TherapySessionNotesUpdatesEventModel(
    Guid AggregateId,
    string Notes
) : IAggregateEventModel;

public static class TherapySessionNotesUpdatesEventModelMapper
{
    public static TherapySessionNotesUpdatesEventModel ToModel(this TherapySessionNotesUpdates domain)
        => new(
            domain.Id.ToGuid(),
            domain.Notes.Content
        );

    public static TherapySessionNotesUpdates ToDomain(this TherapySessionNotesUpdatesEventModel model)
        => new(
            new TherapySessionId(model.AggregateId),
            new SessionNotes(model.Notes)
        );
}
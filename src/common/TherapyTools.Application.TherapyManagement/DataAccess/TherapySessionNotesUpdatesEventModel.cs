using System;
using TherapyTools.Application.TherapyManagement.Interfaces;
using TherapyTools.Domain.TherapyManagement;

public record TherapySessionNotesUpdatesEventModel(
    Guid Id,
    string Notes
) : ITherapySessionEventModel;

public static class TherapySessionNotesUpdatesEventModelMapper
{
    public static TherapySessionNotesUpdatesEventModel ToModel(this TherapySessionNotesUpdates domain)
        => new(
            domain.Id.ToGuid(),
            domain.Notes.Content
        );

    public static TherapySessionNotesUpdates ToDomain(this TherapySessionNotesUpdatesEventModel model)
        => new(
            new TherapySessionId(model.Id),
            new SessionNotes(model.Notes)
        );
}

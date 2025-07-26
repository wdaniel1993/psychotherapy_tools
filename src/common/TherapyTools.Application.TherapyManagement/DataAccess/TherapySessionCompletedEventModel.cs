using System;
using TherapyTools.Application.TherapyManagement.Interfaces;
using TherapyTools.Domain.TherapyManagement;

public record TherapySessionCompletedEventModel(
    Guid Id,
    string Notes
) : ITherapySessionEventModel;

public static class TherapySessionCompletedEventModelMapper
{
    public static TherapySessionCompletedEventModel ToModel(this TherapySessionCompleted domain)
        => new(
            domain.Id.ToGuid(),
            domain.Notes.Content
        );

    public static TherapySessionCompleted ToDomain(this TherapySessionCompletedEventModel model)
        => new(
            new TherapySessionId(model.Id),
            new SessionNotes(model.Notes)
        );
}

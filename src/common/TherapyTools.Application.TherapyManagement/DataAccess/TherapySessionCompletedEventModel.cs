using TherapyTools.Application.Common.Interfaces;
using TherapyTools.Domain.TherapyManagement;

namespace TherapyTools.Application.TherapyManagement.DataAccess;

public record TherapySessionCompletedEventModel(
    Guid AggregateId,
    string Notes
) : IAggregateEventModel;

public static class TherapySessionCompletedEventModelMapper
{
    public static TherapySessionCompletedEventModel ToModel(this TherapySessionCompleted domain)
        => new(
            domain.Id.ToGuid(),
            domain.Notes.Content
        );

    public static TherapySessionCompleted ToDomain(this TherapySessionCompletedEventModel model)
        => new(
            new TherapySessionId(model.AggregateId),
            new SessionNotes(model.Notes)
        );
}
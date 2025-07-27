using TherapyTools.Application.Common.Interfaces;
using TherapyTools.Domain.TherapyManagement;

namespace TherapyTools.Application.TherapyManagement.DataAccess;

public record TherapySessionCanceledEventModel(Guid AggregateId) : IAggregateEventModel;

public static class TherapySessionCanceledEventModelMapper
{
    public static TherapySessionCanceledEventModel ToModel(this TherapySessionCanceled domain)
        => new(domain.Id.ToGuid());

    public static TherapySessionCanceled ToDomain(this TherapySessionCanceledEventModel model)
        => new(new TherapySessionId(model.AggregateId));
}
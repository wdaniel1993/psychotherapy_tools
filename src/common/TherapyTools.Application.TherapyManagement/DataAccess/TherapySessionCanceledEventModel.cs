using System;
using TherapyTools.Application.TherapyManagement.Interfaces;
using TherapyTools.Domain.TherapyManagement;

public record TherapySessionCanceledEventModel(Guid Id) : ITherapySessionEventModel;

public static class TherapySessionCanceledEventModelMapper
{
    public static TherapySessionCanceledEventModel ToModel(this TherapySessionCanceled domain)
        => new(domain.Id.ToGuid());

    public static TherapySessionCanceled ToDomain(this TherapySessionCanceledEventModel model)
        => new(new TherapySessionId(model.Id));
}

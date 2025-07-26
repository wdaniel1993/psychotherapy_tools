using System;
using TherapyTools.Application.TherapyManagement.Interfaces;
using TherapyTools.Domain.TherapyManagement;

public record TherapyPlanDiscardedEventModel(Guid Id) : ITherapyPlanEventModel;

public static class TherapyPlanDiscardedEventModelMapper
{
    public static TherapyPlanDiscardedEventModel ToModel(this TherapyPlanDiscarded domain)
        => new(domain.Id.ToGuid());

    public static TherapyPlanDiscarded ToDomain(this TherapyPlanDiscardedEventModel model)
        => new(new TherapyPlanId(model.Id));
}

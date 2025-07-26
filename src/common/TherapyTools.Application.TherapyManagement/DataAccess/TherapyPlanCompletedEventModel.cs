using System;
using TherapyTools.Application.TherapyManagement.Interfaces;
using TherapyTools.Domain.TherapyManagement;

public record TherapyPlanCompletedEventModel(Guid Id) : ITherapyPlanEventModel;

public static class TherapyPlanCompletedEventModelMapper
{
    public static TherapyPlanCompletedEventModel ToModel(this TherapyPlanCompleted domain)
        => new(domain.Id.ToGuid());

    public static TherapyPlanCompleted ToDomain(this TherapyPlanCompletedEventModel model)
        => new(new TherapyPlanId(model.Id));
}

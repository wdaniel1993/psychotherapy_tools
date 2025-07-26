using System;
using TherapyTools.Application.Common.Interfaces;
using TherapyTools.Domain.TherapyManagement;

public record TherapyPlanActivatedEventModel(Guid Id) : IAggregateEventModel;

public static class TherapyPlanActivatedEventModelMapper
{
    public static TherapyPlanActivatedEventModel ToModel(this TherapyPlanActivated domain)
        => new(domain.Id.ToGuid());

    public static TherapyPlanActivated ToDomain(this TherapyPlanActivatedEventModel model)
        => new(new TherapyPlanId(model.Id));
}

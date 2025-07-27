using TherapyTools.Application.Common.Interfaces;
using TherapyTools.Domain.TherapyManagement;

namespace TherapyTools.Application.TherapyManagement.DataAccess;

public record TherapyPlanActivatedEventModel(Guid AggregateId) : IAggregateEventModel;

public static class TherapyPlanActivatedEventModelMapper
{
    public static TherapyPlanActivatedEventModel ToModel(this TherapyPlanActivated domain)
        => new(domain.Id.ToGuid());

    public static TherapyPlanActivated ToDomain(this TherapyPlanActivatedEventModel model)
        => new(new TherapyPlanId(model.AggregateId));
}
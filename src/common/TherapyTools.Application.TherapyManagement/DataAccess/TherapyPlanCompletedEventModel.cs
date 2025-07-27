using TherapyTools.Application.Common.Interfaces;
using TherapyTools.Domain.TherapyManagement;

namespace TherapyTools.Application.TherapyManagement.DataAccess;

public record TherapyPlanCompletedEventModel(Guid AggregateId) : IAggregateEventModel;

public static class TherapyPlanCompletedEventModelMapper
{
    public static TherapyPlanCompletedEventModel ToModel(this TherapyPlanCompleted domain)
        => new(domain.Id.ToGuid());

    public static TherapyPlanCompleted ToDomain(this TherapyPlanCompletedEventModel model)
        => new(new TherapyPlanId(model.AggregateId));
}
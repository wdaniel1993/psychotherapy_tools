using TherapyTools.Application.Common.Interfaces;
using TherapyTools.Domain.TherapyManagement;

namespace TherapyTools.Application.TherapyManagement.DataAccess;

public record TherapyPlanDiscardedEventModel(Guid AggregateId) : IAggregateEventModel;

public static class TherapyPlanDiscardedEventModelMapper
{
    public static TherapyPlanDiscardedEventModel ToModel(this TherapyPlanDiscarded domain)
        => new(domain.Id.ToGuid());

    public static TherapyPlanDiscarded ToDomain(this TherapyPlanDiscardedEventModel model)
        => new(new TherapyPlanId(model.AggregateId));
}
using TherapyTools.Application.Common.Interfaces;
using TherapyTools.Domain.TherapyManagement;

public record TherapyPlanCreatedEventModel(
    Guid AggregateId,
    List<GoalModel> GoalList,
    string Description
) : IAggregateEventModel;

public static class TherapyPlanCreatedEventModelMapper
{
    public static TherapyPlanCreatedEventModel ToModel(this TherapyPlanCreated domain)
        => new(
            domain.Id.ToGuid(),
            domain.GoalList.ToModelList(),
            domain.Description.Content
        );

    public static TherapyPlanCreated ToDomain(this TherapyPlanCreatedEventModel model)
        => new(
            new TherapyPlanId(model.AggregateId),
            new GoalList(model.GoalList.ToDomainList()),
            new TherapyPlanDescription(model.Description)
        );
}

using TherapyTools.Application.Common.Interfaces;
using TherapyTools.Domain.TherapyManagement;

public record TherapyPlanModel(
    Guid AggregateId,
    List<GoalModel> GoalList,
    string Description,
    TherapyPlanStatus Status
) : IAggregateModel;

public static class TherapyPlanModelMapper
{
    public static TherapyPlanModel ToModel(this TherapyPlanState state)
        => new(
            state.AggregateId.ToGuid(),
            state.GoalList.Goals.ToModelList(),
            state.Description.Content,
            state.Status
        );

    public static TherapyPlanState ToDomain(this TherapyPlanModel model)
        => new(
            new TherapyPlanId(model.AggregateId),
            new GoalList(model.GoalList.ToDomainList()),
            new TherapyPlanDescription(model.Description),
            model.Status
        );
}

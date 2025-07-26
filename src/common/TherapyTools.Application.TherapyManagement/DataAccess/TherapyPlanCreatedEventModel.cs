using System;
using TherapyTools.Application.TherapyManagement.Interfaces;
using TherapyTools.Domain.TherapyManagement;

public record TherapyPlanCreatedEventModel(
    Guid Id,
    List<GoalModel> GoalList,
    string Description
) : ITherapyPlanEventModel;

public static class TherapyPlanCreatedEventModelMapper
{
    public static TherapyPlanCreatedEventModel ToModel(this TherapyPlanCreated domain)
        => new(
            domain.Id.ToGuid(),
            domain.GoalList.Goals.ToModelList(),
            domain.Description.Content
        );

    public static TherapyPlanCreated ToDomain(this TherapyPlanCreatedEventModel model)
        => new(
            new TherapyPlanId(model.Id),
            new GoalList(model.GoalList.ToDomainList()),
            new TherapyPlanDescription(model.Description)
        );
}

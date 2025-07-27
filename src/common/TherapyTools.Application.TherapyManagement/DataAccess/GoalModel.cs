using TherapyTools.Domain.TherapyManagement;

namespace TherapyTools.Application.TherapyManagement.DataAccess;

public record GoalModel(
    string Title,
    string Description
);

public static class GoalModelMapper
{
    public static GoalModel ToModel(this Goal goal)
        => new(goal.Title, goal.Description);

    public static Goal ToDomain(this GoalModel model)
        => new(model.Title, model.Description);

    public static List<GoalModel> ToModelList(this GoalList goalList)
        => goalList.Goals.Select(g => g.ToModel()).ToList();

    public static GoalList ToDomainList(this IEnumerable<GoalModel> models)
        => new(models.Select(m => m.ToDomain()).ToList());
}
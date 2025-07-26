using TherapyTools.Domain.TherapyManagement;

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

    public static List<GoalModel> ToModelList(this IEnumerable<Goal> goals)
        => new(goals.Select(g => g.ToModel()));

    public static List<Goal> ToDomainList(this IEnumerable<GoalModel> models)
        => new(models.Select(m => m.ToDomain()));
}

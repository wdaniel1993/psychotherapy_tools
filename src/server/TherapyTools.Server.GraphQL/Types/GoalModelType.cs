using TherapyTools.Application.TherapyManagement.DataAccess;

namespace TherapyTools.Server.GraphQL.Types;

/// <summary>
/// Represents a goal in a therapy plan, including its title and description.
/// </summary>
public class GoalModelType : ObjectType<GoalModel>
{
    protected override void Configure(IObjectTypeDescriptor<GoalModel> descriptor)
    {
        descriptor.Name("GoalModel");
        descriptor.Description("Represents a goal in a therapy plan, including its title and description.");

        descriptor.Field(f => f.Title)
            .Description("The title of the goal.");
        descriptor.Field(f => f.Description)
            .Description("The description of the goal.");
    }
}

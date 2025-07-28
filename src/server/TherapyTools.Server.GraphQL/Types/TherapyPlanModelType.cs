using TherapyTools.Application.TherapyManagement.DataAccess;

namespace TherapyTools.Server.GraphQL.Types;

/// <summary>
/// Represents a therapy plan, including its goals, description, and status.
/// </summary>
public class TherapyPlanModelType : ObjectType<TherapyPlanModel>
{
    protected override void Configure(IObjectTypeDescriptor<TherapyPlanModel> descriptor)
    {
        descriptor.Name("TherapyPlanModel");
        descriptor.Description("Represents a therapy plan, including its goals, description, and status.");

        descriptor.Field(f => f.AggregateId)
            .Description("The unique identifier of the therapy plan.");
        descriptor.Field(f => f.GoalList)
            .Description("The list of goals for the therapy plan.");
        descriptor.Field(f => f.Description)
            .Description("The description of the therapy plan.");
        descriptor.Field(f => f.Status)
            .Description("The current status of the therapy plan.");
    }
}

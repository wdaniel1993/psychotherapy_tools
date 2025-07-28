using TherapyTools.Application.TherapyManagement.DataAccess;

namespace TherapyTools.Server.GraphQL.Definitions;

/// <summary>
/// Event model for when a therapy plan is created.
/// </summary>
public sealed class TherapyPlanCreatedEventModelTypeDefinition : ObjectType<TherapyPlanCreatedEventModel>
{
    protected override void Configure(IObjectTypeDescriptor<TherapyPlanCreatedEventModel> descriptor)
    {
        descriptor.Name("TherapyPlanCreatedEventModel");
        descriptor.Description("Event model for when a therapy plan is created.");

        descriptor.Field(f => f.AggregateId)
            .Description("The unique identifier of the therapy plan.");
        descriptor.Field(f => f.GoalList)
            .Description("The list of goals for the therapy plan.");
        descriptor.Field(f => f.Description)
            .Description("The description of the therapy plan.");
    }
}

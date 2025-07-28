using TherapyTools.Application.TherapyManagement.Commands;

namespace TherapyTools.Server.GraphQL.Definitions;

/// <summary>
/// Command to create a new therapy plan.
/// </summary>
public sealed class CreateTherapyPlanCommandTypeDefinition : ObjectType<CreateTherapyPlanCommand>
{
    protected override void Configure(IObjectTypeDescriptor<CreateTherapyPlanCommand> descriptor)
    {
        descriptor.Name("CreateTherapyPlanCommand");
        descriptor.Description("Command to create a new therapy plan.");

        descriptor.Field(f => f.Id)
            .Description("The unique identifier for the therapy plan.");
        descriptor.Field(f => f.GoalList)
            .Description("The list of goals for the therapy plan.");
        descriptor.Field(f => f.Description)
            .Description("The description of the therapy plan.");
    }
}

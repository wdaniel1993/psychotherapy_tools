using HotChocolate;
using HotChocolate.Types;
using TherapyTools.Application.TherapyManagement.Commands;

namespace TherapyTools.Server.GraphQL.Types;

/// <summary>
/// Command to create a new therapy plan.
/// </summary>
public class CreateTherapyPlanCommandType : ObjectType<CreateTherapyPlanCommand>
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

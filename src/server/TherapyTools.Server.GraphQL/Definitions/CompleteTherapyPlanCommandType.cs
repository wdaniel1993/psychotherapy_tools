using TherapyTools.Application.TherapyManagement.Commands;

namespace TherapyTools.Server.GraphQL.Definitions;

/// <summary>
/// Command to complete a therapy plan.
/// </summary>
public class CompleteTherapyPlanCommandType : ObjectType<CompleteTherapyPlanCommand>
{
    protected override void Configure(IObjectTypeDescriptor<CompleteTherapyPlanCommand> descriptor)
    {
        descriptor.Name("CompleteTherapyPlanCommand");
        descriptor.Description("Command to complete a therapy plan.");

        descriptor.Field(f => f.Id)
            .Description("The unique identifier for the therapy plan.");
    }
}

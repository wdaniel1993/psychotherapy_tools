using TherapyTools.Application.TherapyManagement.Commands;

namespace TherapyTools.Server.GraphQL.Types;

/// <summary>
/// Command to activate a therapy plan.
/// </summary>
public class ActivateTherapyPlanCommandType : ObjectType<ActivateTherapyPlanCommand>
{
    protected override void Configure(IObjectTypeDescriptor<ActivateTherapyPlanCommand> descriptor)
    {
        descriptor.Name("ActivateTherapyPlanCommand");
        descriptor.Description("Command to activate a therapy plan.");

        descriptor.Field(f => f.Id)
            .Description("The unique identifier for the therapy plan.");
    }
}

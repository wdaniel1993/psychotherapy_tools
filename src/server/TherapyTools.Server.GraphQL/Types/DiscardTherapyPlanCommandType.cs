using TherapyTools.Application.TherapyManagement.Commands;

namespace TherapyTools.Server.GraphQL.Types;

/// <summary>
/// Command to discard a therapy plan.
/// </summary>
public class DiscardTherapyPlanCommandType : ObjectType<DiscardTherapyPlanCommand>
{
    protected override void Configure(IObjectTypeDescriptor<DiscardTherapyPlanCommand> descriptor)
    {
        descriptor.Name("DiscardTherapyPlanCommand");
        descriptor.Description("Command to discard a therapy plan.");

        descriptor.Field(f => f.Id)
            .Description("The unique identifier for the therapy plan.");
    }
}

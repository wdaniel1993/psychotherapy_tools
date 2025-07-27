using HotChocolate;
using HotChocolate.Types;
using TherapyTools.Application.TherapyManagement.Commands;

namespace TherapyTools.Server.GraphQL.Types;

/// <summary>
/// Command to cancel a therapy session.
/// </summary>
public class CancelTherapySessionCommandType : ObjectType<CancelTherapySessionCommand>
{
    protected override void Configure(IObjectTypeDescriptor<CancelTherapySessionCommand> descriptor)
    {
        descriptor.Name("CancelTherapySessionCommand");
        descriptor.Description("Command to cancel a therapy session.");

        descriptor.Field(f => f.Id)
            .Description("The unique identifier for the therapy session.");
    }
}

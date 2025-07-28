using TherapyTools.Application.TherapyManagement.Commands;

namespace TherapyTools.Server.GraphQL.Definitions;

/// <summary>
/// Command to complete a therapy session.
/// </summary>
public sealed class CompleteTherapySessionCommandTypeDefinition : ObjectType<CompleteTherapySessionCommand>
{
    protected override void Configure(IObjectTypeDescriptor<CompleteTherapySessionCommand> descriptor)
    {
        descriptor.Name("CompleteTherapySessionCommand");
        descriptor.Description("Command to complete a therapy session.");

        descriptor.Field(f => f.Id)
            .Description("The unique identifier for the therapy session.");
        descriptor.Field(f => f.Notes)
            .Description("Notes for the completed therapy session.");
    }
}

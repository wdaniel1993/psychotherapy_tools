using TherapyTools.Application.TherapyManagement.Commands;

namespace TherapyTools.Server.GraphQL.Definitions;

/// <summary>
/// Command to update notes for a therapy session.
/// </summary>
public sealed class UpdateTherapySessionNotesCommandTypeDefinition : ObjectType<UpdateTherapySessionNotesCommand>
{
    protected override void Configure(IObjectTypeDescriptor<UpdateTherapySessionNotesCommand> descriptor)
    {
        descriptor.Name("UpdateTherapySessionNotesCommand");
        descriptor.Description("Command to update notes for a therapy session.");

        descriptor.Field(f => f.Id)
            .Description("The unique identifier for the therapy session.");
        descriptor.Field(f => f.Notes)
            .Description("The updated notes for the therapy session.");
    }
}

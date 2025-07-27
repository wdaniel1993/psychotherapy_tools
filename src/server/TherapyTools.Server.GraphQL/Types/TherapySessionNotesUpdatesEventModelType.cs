using HotChocolate;
using HotChocolate.Types;
using TherapyTools.Application.TherapyManagement.DataAccess;

namespace TherapyTools.Server.GraphQL.Types;

/// <summary>
/// Event model for when notes are updated for a therapy session.
/// </summary>
public class TherapySessionNotesUpdatesEventModelType : ObjectType<TherapySessionNotesUpdatesEventModel>
{
    protected override void Configure(IObjectTypeDescriptor<TherapySessionNotesUpdatesEventModel> descriptor)
    {
        descriptor.Name("TherapySessionNotesUpdatesEventModel");
        descriptor.Description("Event model for when notes are updated for a therapy session.");

        descriptor.Field(f => f.AggregateId)
            .Description("The unique identifier of the therapy session.");
        descriptor.Field(f => f.Notes)
            .Description("The updated notes for the therapy session.");
    }
}

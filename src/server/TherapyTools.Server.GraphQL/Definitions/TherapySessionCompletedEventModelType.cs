using TherapyTools.Application.TherapyManagement.DataAccess;

namespace TherapyTools.Server.GraphQL.Definitions;

/// <summary>
/// Event model for when a therapy session is completed.
/// </summary>
public class TherapySessionCompletedEventModelType : ObjectType<TherapySessionCompletedEventModel>
{
    protected override void Configure(IObjectTypeDescriptor<TherapySessionCompletedEventModel> descriptor)
    {
        descriptor.Name("TherapySessionCompletedEventModel");
        descriptor.Description("Event model for when a therapy session is completed.");

        descriptor.Field(f => f.AggregateId)
            .Description("The unique identifier of the therapy session.");
        descriptor.Field(f => f.Notes)
            .Description("Notes for the completed therapy session.");
    }
}

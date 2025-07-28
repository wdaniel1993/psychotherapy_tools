using TherapyTools.Application.TherapyManagement.DataAccess;

namespace TherapyTools.Server.GraphQL.Definitions;

/// <summary>
/// Event model for when a therapy session is canceled.
/// </summary>
public sealed class TherapySessionCanceledEventModelTypeDefinition : ObjectType<TherapySessionCanceledEventModel>
{
    protected override void Configure(IObjectTypeDescriptor<TherapySessionCanceledEventModel> descriptor)
    {
        descriptor.Name("TherapySessionCanceledEventModel");
        descriptor.Description("Event model for when a therapy session is canceled.");

        descriptor.Field(f => f.AggregateId)
            .Description("The unique identifier of the therapy session.");
    }
}

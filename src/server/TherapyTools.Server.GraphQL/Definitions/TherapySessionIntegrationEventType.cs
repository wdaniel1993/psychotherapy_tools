using TherapyTools.Application.TherapyManagement.IntegrationEvents;

namespace TherapyTools.Server.GraphQL.Definitions;

/// <summary>
/// GraphQL type for TherapySessionIntegrationEvent.
/// </summary>
public sealed class TherapySessionIntegrationEventTypeDefinition : ObjectType<TherapySessionIntegrationEvent>
{
    protected override void Configure(IObjectTypeDescriptor<TherapySessionIntegrationEvent> descriptor)
    {
        descriptor.Name("TherapySessionIntegrationEvent");
        descriptor.Description("Represents an integration event for a therapy session.");

        descriptor.Field(f => f.EventName)
            .Description("The name of the integration event.");
        descriptor.Field(f => f.EventType)
            .Type<IntegrationEventTypeEnumTypeDefinition>()
            .Description("The type of the integration event.");
        descriptor.Field(f => f.Event)
            .Type<AggregateEventModelTypeDefinition>()
            .Description("The event data for the therapy session.");
        descriptor.Field(f => f.State)
            .Type<TherapySessionModelTypeDefinition>()
            .Description("The current state of the therapy session.");
    }
}

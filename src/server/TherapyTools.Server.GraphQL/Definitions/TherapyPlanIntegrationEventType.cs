using TherapyTools.Application.TherapyManagement.IntegrationEvents;

namespace TherapyTools.Server.GraphQL.Definitions;

/// <summary>
/// GraphQL type for TherapyPlanIntegrationEvent.
/// </summary>
public sealed class TherapyPlanIntegrationEventTypeDefinition : ObjectType<TherapyPlanIntegrationEvent>
{
    protected override void Configure(IObjectTypeDescriptor<TherapyPlanIntegrationEvent> descriptor)
    {
        descriptor.Name("TherapyPlanIntegrationEvent");
        descriptor.Description("Represents an integration event for a therapy plan.");

        descriptor.Field(f => f.EventName)
            .Description("The name of the integration event.");
        descriptor.Field(f => f.EventType)
            .Type<IntegrationEventTypeEnumTypeDefinition>()
            .Description("The type of the integration event.");
        descriptor.Field(f => f.Event)
            .Type<AggregateEventModelTypeDefinition>()
            .Description("The event data for the therapy plan.");
        descriptor.Field(f => f.State)
            .Type<TherapyPlanModelTypeDefinition>()
            .Description("The current state of the therapy plan.");
    }
}

using TherapyTools.Application.Common.Interfaces;

namespace TherapyTools.Server.GraphQL.Definitions;

/// <summary>
/// Represents a general integration event, including event name and type.
/// </summary>
public sealed class IntegrationEventTypeDefinition : InterfaceType<IIntegrationEvent>
{
    protected override void Configure(IInterfaceTypeDescriptor<IIntegrationEvent> descriptor)
    {
        descriptor.Name("IntegrationEvent");
        descriptor.Description("Represents a general integration event, including event name and type.");

        descriptor.Field(f => f.EventName)
            .Description("The name of the integration event.");
        descriptor.Field(f => f.EventType)
            .Description("The type of the integration event.");
    }
}

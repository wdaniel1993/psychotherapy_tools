using TherapyTools.Application.Common.Interfaces;

namespace TherapyTools.Server.GraphQL.Types;

/// <summary>
/// Represents an integration event for an aggregate, including event name, type, and aggregate ID.
/// </summary>
public class AggregateIntegrationEventType : InterfaceType<IAggregateIntegrationEvent>
{
    protected override void Configure(IInterfaceTypeDescriptor<IAggregateIntegrationEvent> descriptor)
    {
        descriptor.Name("AggregateIntegrationEvent");
        descriptor.Description("Represents an integration event for an aggregate, including event name, type, and aggregate ID.");

        descriptor.Field(f => f.EventName)
            .Description("The name of the integration event.");
        descriptor.Field(f => f.EventType)
            .Description("The type of the integration event.");
        descriptor.Field(f => f.AggregateId)
            .Description("The unique identifier of the aggregate affected by the event.");
    }
}

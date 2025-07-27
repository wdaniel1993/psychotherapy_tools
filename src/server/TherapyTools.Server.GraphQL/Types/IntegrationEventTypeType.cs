using HotChocolate;
using HotChocolate.Types;
using TherapyTools.Application.Common.Interfaces;
using TherapyTools.Application.Common;

namespace TherapyTools.Server.GraphQL.Types;

/// <summary>
/// Represents a general integration event, including event name and type.
/// </summary>
public class IntegrationEventTypeType : InterfaceType<IIntegrationEvent>
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

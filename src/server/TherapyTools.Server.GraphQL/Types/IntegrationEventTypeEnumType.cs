using TherapyTools.Application.Common;

namespace TherapyTools.Server.GraphQL.Types;

/// <summary>
/// Represents the type of integration event.
/// </summary>
public class IntegrationEventTypeEnumType : EnumType<IntegrationEventType>
{
    protected override void Configure(IEnumTypeDescriptor<IntegrationEventType> descriptor)
    {
        descriptor.Name("IntegrationEventType");
        descriptor.Description("The type of integration event.");

        descriptor.Value(IntegrationEventType.Created)
            .Description("Indicates the entity was created.");
        descriptor.Value(IntegrationEventType.Updated)
            .Description("Indicates the entity was updated.");
        descriptor.Value(IntegrationEventType.Deleted)
            .Description("Indicates the entity was deleted.");
        descriptor.Value(IntegrationEventType.Refresh)
            .Description("Indicates the entity should be refreshed.");
    }
}

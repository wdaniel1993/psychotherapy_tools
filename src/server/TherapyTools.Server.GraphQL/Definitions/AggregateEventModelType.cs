using TherapyTools.Application.Common.Interfaces;

namespace TherapyTools.Server.GraphQL.Definitions;

/// <summary>
/// Represents an event model for an aggregate, including its unique identifier.
/// </summary>
public sealed class AggregateEventModelTypeDefinition : InterfaceType<IAggregateEventModel>
{
    protected override void Configure(IInterfaceTypeDescriptor<IAggregateEventModel> descriptor)
    {
        descriptor.Name("AggregateEventModel");
        descriptor.Description("Represents an event model for an aggregate, including its unique identifier.");

        descriptor.Field(f => f.AggregateId)
            .Description("The unique identifier of the aggregate for this event model.");
    }
}

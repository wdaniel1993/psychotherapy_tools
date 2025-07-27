using HotChocolate;
using HotChocolate.Types;
using TherapyTools.Application.Common.Interfaces;

namespace TherapyTools.Server.GraphQL.Types;

/// <summary>
/// Represents an event model for an aggregate, including its unique identifier.
/// </summary>
public class AggregateEventModelType : InterfaceType<IAggregateEventModel>
{
    protected override void Configure(IInterfaceTypeDescriptor<IAggregateEventModel> descriptor)
    {
        descriptor.Name("AggregateEventModel");
        descriptor.Description("Represents an event model for an aggregate, including its unique identifier.");

        descriptor.Field(f => f.AggregateId)
            .Description("The unique identifier of the aggregate for this event model.");
    }
}

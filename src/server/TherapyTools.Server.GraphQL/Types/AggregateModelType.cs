using TherapyTools.Application.Common.Interfaces;

namespace TherapyTools.Server.GraphQL.Types;

/// <summary>
/// Represents an aggregate model with a unique identifier.
/// </summary>
public class AggregateModelType : InterfaceType<IAggregateModel>
{
    protected override void Configure(IInterfaceTypeDescriptor<IAggregateModel> descriptor)
    {
        descriptor.Name("AggregateModel");
        descriptor.Description("Represents an aggregate model with a unique identifier.");

        descriptor.Field(f => f.AggregateId)
            .Description("The unique identifier of the aggregate model.");
    }
}

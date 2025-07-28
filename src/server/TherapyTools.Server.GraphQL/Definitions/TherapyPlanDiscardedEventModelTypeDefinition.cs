using TherapyTools.Application.TherapyManagement.DataAccess;

namespace TherapyTools.Server.GraphQL.Definitions;

/// <summary>
/// Event model for when a therapy plan is discarded.
/// </summary>
public sealed class TherapyPlanDiscardedEventModelTypeDefinition : ObjectType<TherapyPlanDiscardedEventModel>
{
    protected override void Configure(IObjectTypeDescriptor<TherapyPlanDiscardedEventModel> descriptor)
    {
        descriptor.Name("TherapyPlanDiscardedEventModel");
        descriptor.Description("Event model for when a therapy plan is discarded.");

        descriptor.Field(f => f.AggregateId)
            .Description("The unique identifier of the therapy plan.");
    }
}

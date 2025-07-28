using TherapyTools.Application.TherapyManagement.DataAccess;

namespace TherapyTools.Server.GraphQL.Definitions;

/// <summary>
/// Event model for when a therapy plan is completed.
/// </summary>
public class TherapyPlanCompletedEventModelType : ObjectType<TherapyPlanCompletedEventModel>
{
    protected override void Configure(IObjectTypeDescriptor<TherapyPlanCompletedEventModel> descriptor)
    {
        descriptor.Name("TherapyPlanCompletedEventModel");
        descriptor.Description("Event model for when a therapy plan is completed.");

        descriptor.Field(f => f.AggregateId)
            .Description("The unique identifier of the therapy plan.");
    }
}

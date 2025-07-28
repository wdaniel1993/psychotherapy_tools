using TherapyTools.Application.TherapyManagement.DataAccess;

namespace TherapyTools.Server.GraphQL.Types;

/// <summary>
/// Event model for when a therapy plan is activated.
/// </summary>
public class TherapyPlanActivatedEventModelType : ObjectType<TherapyPlanActivatedEventModel>
{
    protected override void Configure(IObjectTypeDescriptor<TherapyPlanActivatedEventModel> descriptor)
    {
        descriptor.Name("TherapyPlanActivatedEventModel");
        descriptor.Description("Event model for when a therapy plan is activated.");

        descriptor.Field(f => f.AggregateId)
            .Description("The unique identifier of the therapy plan.");
    }
}

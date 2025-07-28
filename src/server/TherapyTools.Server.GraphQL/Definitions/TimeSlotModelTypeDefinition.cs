using TherapyTools.Application.TherapyManagement.DataAccess;

namespace TherapyTools.Server.GraphQL.Definitions;

/// <summary>
/// Represents a time slot with a start and end date/time.
/// </summary>
public sealed class TimeSlotModelTypeDefinition : ObjectType<TimeSlotModel>
{
    protected override void Configure(IObjectTypeDescriptor<TimeSlotModel> descriptor)
    {
        descriptor.Name("TimeSlotModel");
        descriptor.Description("Represents a time slot with a start and end date/time.");

        descriptor.Field(f => f.Start)
            .Description("The start date and time of the time slot.");
        descriptor.Field(f => f.End)
            .Description("The end date and time of the time slot.");
    }
}

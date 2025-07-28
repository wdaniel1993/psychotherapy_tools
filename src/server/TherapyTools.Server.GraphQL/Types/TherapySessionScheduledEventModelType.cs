using TherapyTools.Application.TherapyManagement.DataAccess;

namespace TherapyTools.Server.GraphQL.Types;

/// <summary>
/// Event model for when a therapy session is scheduled.
/// </summary>
public class TherapySessionScheduledEventModelType : ObjectType<TherapySessionScheduledEventModel>
{
    protected override void Configure(IObjectTypeDescriptor<TherapySessionScheduledEventModel> descriptor)
    {
        descriptor.Name("TherapySessionScheduledEventModel");
        descriptor.Description("Event model for when a therapy session is scheduled.");

        descriptor.Field(f => f.AggregateId)
            .Description("The unique identifier of the therapy session.");
        descriptor.Field(f => f.SessionTimeSlot)
            .Description("The time slot for the therapy session.");
        descriptor.Field(f => f.Type)
            .Description("The type of the therapy session.");
        descriptor.Field(f => f.Notes)
            .Description("Notes for the therapy session.");
    }
}

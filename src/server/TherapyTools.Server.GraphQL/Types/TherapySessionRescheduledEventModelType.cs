using HotChocolate;
using HotChocolate.Types;
using TherapyTools.Application.TherapyManagement.DataAccess;

namespace TherapyTools.Server.GraphQL.Types;

/// <summary>
/// Event model for when a therapy session is rescheduled.
/// </summary>
public class TherapySessionRescheduledEventModelType : ObjectType<TherapySessionRescheduledEventModel>
{
    protected override void Configure(IObjectTypeDescriptor<TherapySessionRescheduledEventModel> descriptor)
    {
        descriptor.Name("TherapySessionRescheduledEventModel");
        descriptor.Description("Event model for when a therapy session is rescheduled.");

        descriptor.Field(f => f.AggregateId)
            .Description("The unique identifier of the therapy session.");
        descriptor.Field(f => f.NewSlot)
            .Description("The new time slot for the therapy session.");
    }
}

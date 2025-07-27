using HotChocolate;
using HotChocolate.Types;
using TherapyTools.Application.TherapyManagement;
using TherapyTools.Application.TherapyManagement.DataAccess;

namespace TherapyTools.Server.GraphQL.Types;

/// <summary>
/// Represents a therapy session, including its time slot, type, notes, and status.
/// </summary>
public class TherapySessionModelType : ObjectType<TherapySessionModel>
{
    protected override void Configure(IObjectTypeDescriptor<TherapySessionModel> descriptor)
    {
        descriptor.Name("TherapySessionModel");
        descriptor.Description("Represents a therapy session, including its time slot, type, notes, and status.");

        descriptor.Field(f => f.AggregateId)
            .Description("The unique identifier of the therapy session.");
        descriptor.Field(f => f.SessionTimeSlot)
            .Description("The time slot for the therapy session.");
        descriptor.Field(f => f.Type)
            .Description("The type of the therapy session.");
        descriptor.Field(f => f.Notes)
            .Description("Notes for the therapy session.");
        descriptor.Field(f => f.Status)
            .Description("The current status of the therapy session.");
    }
}

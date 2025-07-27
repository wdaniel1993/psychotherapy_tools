using HotChocolate;
using HotChocolate.Types;
using TherapyTools.Application.TherapyManagement.Commands;
using TherapyTools.Domain.TherapyManagement.ValueObjects;

namespace TherapyTools.Server.GraphQL.Types;

/// <summary>
/// Command to schedule a new therapy session.
/// </summary>
public class ScheduleTherapySessionCommandType : ObjectType<ScheduleTherapySessionCommand>
{
    protected override void Configure(IObjectTypeDescriptor<ScheduleTherapySessionCommand> descriptor)
    {
        descriptor.Name("ScheduleTherapySessionCommand");
        descriptor.Description("Command to schedule a new therapy session.");

        descriptor.Field(f => f.Id)
            .Description("The unique identifier for the therapy session.");
        descriptor.Field(f => f.SessionTimeSlot)
            .Description("The time slot for the therapy session.");
        descriptor.Field(f => f.Type)
            .Description("The type of the therapy session.");
        descriptor.Field(f => f.Notes)
            .Description("Notes for the therapy session.");
    }
}

using TherapyTools.Application.TherapyManagement.Commands;

namespace TherapyTools.Server.GraphQL.Definitions;

/// <summary>
/// Command to schedule a new therapy session.
/// </summary>
public sealed class ScheduleTherapySessionCommandTypeDefinition : ObjectType<ScheduleTherapySessionCommand>
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

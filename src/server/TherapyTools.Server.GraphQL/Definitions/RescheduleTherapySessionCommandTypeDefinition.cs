using TherapyTools.Application.TherapyManagement.Commands;

namespace TherapyTools.Server.GraphQL.Definitions;

/// <summary>
/// Command to reschedule an existing therapy session.
/// </summary>
public sealed class RescheduleTherapySessionCommandTypeDefinition : ObjectType<RescheduleTherapySessionCommand>
{
    protected override void Configure(IObjectTypeDescriptor<RescheduleTherapySessionCommand> descriptor)
    {
        descriptor.Name("RescheduleTherapySessionCommand");
        descriptor.Description("Command to reschedule an existing therapy session.");

        descriptor.Field(f => f.Id)
            .Description("The unique identifier for the therapy session.");
        descriptor.Field(f => f.NewSlot)
            .Description("The new time slot for the therapy session.");
    }
}

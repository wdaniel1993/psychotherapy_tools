using TherapyTools.Domain.TherapyManagement;

namespace TherapyTools.Server.GraphQL.Types;

/// <summary>
/// Represents the type of therapy session.
/// </summary>
public class TherapySessionTypeType : EnumType<TherapySessionType>
{
    protected override void Configure(IEnumTypeDescriptor<TherapySessionType> descriptor)
    {
        descriptor.Name("TherapySessionType");
        descriptor.Description("The type of therapy session.");

        descriptor.Value(TherapySessionType.Individual)
            .Description("An individual therapy session.");
        descriptor.Value(TherapySessionType.Group)
            .Description("A group therapy session.");
        descriptor.Value(TherapySessionType.Family)
            .Description("A family therapy session.");
    }
}

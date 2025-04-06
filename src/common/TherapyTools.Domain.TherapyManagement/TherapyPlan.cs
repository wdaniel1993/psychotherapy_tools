using TherapyTools.Domain.Common.Cqrs;

namespace TherapyTools.Domain.TherapyManagement;

public record TherapyPlan
{
    public Guid Id { get; init; }
    public required string Objectives { get; init; }
    public required List<string> Milestones { get; init; }
    public required List<string> TherapyMethods { get; init; }

    public TherapyPlan UpdatePlan(string newObjectives, List<string> newMilestones, List<string> newTherapyMethods) =>
        this with { Objectives = newObjectives, Milestones = newMilestones, TherapyMethods = newTherapyMethods };
}

public record TherapyPlanUpdated(Guid Id, string NewObjectives, List<string> NewMilestones, List<string> NewTherapyMethods) : IDomainEvent;

public record UpdateTherapyPlanCommand(Guid Id, string NewObjectives, List<string> NewMilestones, List<string> NewTherapyMethods);

public class TherapyPlanCommandHandler
{
    public static IEnumerable<IDomainEvent> Handle(UpdateTherapyPlanCommand command)
    {
        yield return new TherapyPlanUpdated(command.Id, command.NewObjectives, command.NewMilestones, command.NewTherapyMethods);
    }
}

public class TherapyPlanEventHandler
{
    public static TherapyPlan Apply(TherapyPlanUpdated @event, TherapyPlan plan) =>
        plan.UpdatePlan(@event.NewObjectives, @event.NewMilestones, @event.NewTherapyMethods);
}
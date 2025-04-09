using TherapyTools.Domain.Common.Cqrs;

namespace TherapyTools.Domain.TherapyManagement.CommandHandlers.TherapyPlan;

public class CreateTherapyPlanCommandHandler : IAggregateCommandHandler<CreateTherapyPlanCommand, TherapyPlanId, TherapyPlanState>
{
    public async IAsyncEnumerable<IDomainEvent> Handle(CreateTherapyPlanCommand command, TherapyPlanState state)
    {
        if(state.Status != TherapyPlanStatus.Draft)
            throw new InvalidOperationException("Therapy plan must be in draft status to be created.");
        yield return new TherapyPlanCreated(command.Id, command.GoalList, command.Description);
    }
}
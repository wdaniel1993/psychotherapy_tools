using TherapyTools.Domain.Common.Cqrs;

namespace TherapyTools.Domain.TherapyManagement.CommandHandlers.TherapyPlan;

public class ActivateTherapyPlanCommandHandler : IAggregateCommandHandler<ActivateTherapyPlanCommand, TherapyPlanId, TherapyPlanState>
{
    public async IAsyncEnumerable<IDomainEvent> Handle(ActivateTherapyPlanCommand command, TherapyPlanState state)
    {
        if(state.Status != TherapyPlanStatus.Draft)
            throw new InvalidOperationException("Therapy plan must be in draft status to be activated.");
        yield return new TherapyPlanActivated(command.Id);
    }
}
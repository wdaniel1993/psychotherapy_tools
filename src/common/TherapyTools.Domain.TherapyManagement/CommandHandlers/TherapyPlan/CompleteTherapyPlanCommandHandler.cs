using TherapyTools.Domain.Common.Cqrs;

namespace TherapyTools.Domain.TherapyManagement.CommandHandlers.TherapyPlan;

public class CompleteTherapyPlanCommandHandler : IAggregateCommandHandler<CompleteTherapyPlanCommand, TherapyPlanId, TherapyPlanState>
{
    public Task<IEnumerable<IDomainEvent>> Handle(CompleteTherapyPlanCommand command, TherapyPlanState state)
    {
        if(state.Status != TherapyPlanStatus.Active)
            throw new InvalidOperationException("Therapy plan must be in active status to be completed.");
        return Task.FromResult<IEnumerable<IDomainEvent>>([new TherapyPlanCompleted(command.Id)]);
    }
}
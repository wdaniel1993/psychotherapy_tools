using TherapyTools.Application.Common.Interfaces;
using TherapyTools.Domain.Common.Interfaces;
using TherapyTools.Domain.TherapyManagement;

namespace TherapyTools.Application.TherapyManagement.Commands.Handlers.TherapyPlan;

public class CompleteTherapyPlanCommandHandler : IAggregateCommandHandler<CompleteTherapyPlanCommand, TherapyPlanId, TherapyPlanState>
{
    public Task<IEnumerable<IDomainEvent>> Handle(CompleteTherapyPlanCommand command, TherapyPlanState state)
    {
        if(state.Status != TherapyPlanStatus.Active)
            throw new InvalidOperationException("Therapy plan must be in active status to be completed.");
        return Task.FromResult<IEnumerable<IDomainEvent>>([new TherapyPlanCompleted(command.Id)]);
    }
}
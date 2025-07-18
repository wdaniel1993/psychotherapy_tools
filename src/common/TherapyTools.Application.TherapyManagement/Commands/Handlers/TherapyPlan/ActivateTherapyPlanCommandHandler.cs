using TherapyTools.Application.Common.Interfaces;
using TherapyTools.Domain.Common.Interfaces;
using TherapyTools.Domain.TherapyManagement;

namespace TherapyTools.Application.TherapyManagement.Commands.Handlers.TherapyPlan;

public class ActivateTherapyPlanCommandHandler : IAggregateCommandHandler<ActivateTherapyPlanCommand, TherapyPlanId, TherapyPlanState>
{
    public Task<IEnumerable<IDomainEvent>> Handle(ActivateTherapyPlanCommand command, TherapyPlanState state)
    {
        if(state.Status != TherapyPlanStatus.Draft)
            throw new InvalidOperationException("Therapy plan must be in draft status to be activated.");
        return Task.FromResult<IEnumerable<IDomainEvent>>([new TherapyPlanActivated(command.Id)]);
    }
}
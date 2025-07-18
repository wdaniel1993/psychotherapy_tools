using TherapyTools.Application.Common.Interfaces;
using TherapyTools.Domain.Common.Interfaces;
using TherapyTools.Domain.TherapyManagement;

namespace TherapyTools.Application.TherapyManagement.Commands.Handlers.TherapyPlan;

public class DiscardTherapyPlanCommandHandler : IAggregateCommandHandler<DiscardTherapyPlanCommand, TherapyPlanId, TherapyPlanState>
{
    public Task<IEnumerable<IDomainEvent>> Handle(DiscardTherapyPlanCommand command, TherapyPlanState state)
    {
        if (state.Status == TherapyPlanStatus.Completed)
            throw new InvalidOperationException("Therapy plan cannot be discarded after completion.");
        return Task.FromResult<IEnumerable<IDomainEvent>>([new TherapyPlanDiscard(command.Id)]);
    }
}
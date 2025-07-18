using TherapyTools.Domain.Common.Cqrs;

namespace TherapyTools.Domain.TherapyManagement.CommandHandlers.TherapyPlan;

public class DiscardTherapyPlanCommandHandler : IAggregateCommandHandler<DiscardTherapyPlanCommand, TherapyPlanId, TherapyPlanState>
{
    public Task<IEnumerable<IDomainEvent>> Handle(DiscardTherapyPlanCommand command, TherapyPlanState state)
    {
        if (state.Status == TherapyPlanStatus.Completed)
            throw new InvalidOperationException("Therapy plan cannot be discarded after completion.");
        return Task.FromResult<IEnumerable<IDomainEvent>>([new TherapyPlanDiscard(command.Id)]);
    }
}
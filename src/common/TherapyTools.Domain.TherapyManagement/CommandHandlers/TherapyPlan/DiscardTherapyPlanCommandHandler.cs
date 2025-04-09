using TherapyTools.Domain.Common.Cqrs;

namespace TherapyTools.Domain.TherapyManagement.CommandHandlers.TherapyPlan;

public class DiscardTherapyPlanCommandHandler : IAggregateCommandHandler<DiscardTherapyPlanCommand, TherapyPlanId, TherapyPlanState>
{
    public async IAsyncEnumerable<IDomainEvent> Handle(DiscardTherapyPlanCommand command, TherapyPlanState state)
    {
        if (state.Status == TherapyPlanStatus.Completed)
            throw new InvalidOperationException("Therapy plan cannot be discarded after completion.");
        yield return new TherapyPlanDiscard(command.Id);
    }
}
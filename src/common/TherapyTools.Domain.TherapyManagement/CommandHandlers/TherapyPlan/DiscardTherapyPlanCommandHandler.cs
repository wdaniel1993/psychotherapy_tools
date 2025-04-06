using TherapyTools.Domain.Common.Cqrs;

namespace TherapyTools.Domain.TherapyManagement.CommandHandlers.TherapyPlan;

public class DiscardTherapyPlanCommandHandler : ICommandHandler<DiscardTherapyPlanCommand>
{
    private readonly IEventStore _eventStore;

    public DiscardTherapyPlanCommandHandler(IEventStore eventStore)
    {
        _eventStore = eventStore;
    }

    public async IAsyncEnumerable<IDomainEvent> Handle(DiscardTherapyPlanCommand command)
    {
        var currentState = await TherapyPlanAggregate.GetCurrentState(_eventStore, command.Id);
        if (currentState.Status == TherapyPlanStatus.Completed)
            throw new InvalidOperationException("Therapy plan cannot be discarded after completion.");
        yield return new TherapyPlanDiscard(command.Id);
    }
}
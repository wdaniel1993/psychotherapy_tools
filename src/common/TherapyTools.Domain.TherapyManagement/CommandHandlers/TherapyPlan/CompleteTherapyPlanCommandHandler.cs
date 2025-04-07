using TherapyTools.Domain.Common.Cqrs;

namespace TherapyTools.Domain.TherapyManagement.CommandHandlers.TherapyPlan;

public class CompleteTherapyPlanCommandHandler : ICommandHandler<CompleteTherapyPlanCommand>
{
    private readonly IEventStore<TherapyPlanId> _eventStore;

    public CompleteTherapyPlanCommandHandler(IEventStore<TherapyPlanId> eventStore)
    {
        _eventStore = eventStore;
    }

    public async IAsyncEnumerable<IDomainEvent> Handle(CompleteTherapyPlanCommand command)
    {
        var currentState = await TherapyPlanAggregate.GetCurrentState(_eventStore, command.Id);
        if(currentState.Status != TherapyPlanStatus.Active)
            throw new InvalidOperationException("Therapy plan must be in active status to be completed.");
        yield return new TherapyPlanCompleted(command.Id);
    }
}
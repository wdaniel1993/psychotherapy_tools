using TherapyTools.Domain.Common.Cqrs;

namespace TherapyTools.Domain.TherapyManagement.CommandHandlers.TherapyPlan;

public class ActivateTherapyPlanCommandHandler : ICommandHandler<ActivateTherapyPlanCommand>
{
    private readonly IEventStore _eventStore;

    public ActivateTherapyPlanCommandHandler(IEventStore eventStore)
    {
        _eventStore = eventStore;
    }

    public async IAsyncEnumerable<IDomainEvent> Handle(ActivateTherapyPlanCommand command)
    {
        var currentState = await TherapyPlanAggregate.GetCurrentState(_eventStore, command.Id);
        if(currentState.Status != TherapyPlanStatus.Draft)
            throw new InvalidOperationException("Therapy plan must be in draft status to be activated.");
        yield return new TherapyPlanActivated(command.Id);
    }
}
using TherapyTools.Domain.Common.Cqrs;

namespace TherapyTools.Domain.TherapyManagement.CommandHandlers.TherapyPlan;

public class CreateTherapyPlanCommandHandler : ICommandHandler<CreateTherapyPlanCommand>
{
    private readonly IEventStore _eventStore;

    public CreateTherapyPlanCommandHandler(IEventStore eventStore)
    {
        _eventStore = eventStore;
    }

    public async IAsyncEnumerable<IDomainEvent> Handle(CreateTherapyPlanCommand command)
    {
        var currentState = await TherapyPlanAggregate.GetCurrentState(_eventStore, command.Id);
        if(currentState.Status != TherapyPlanStatus.Draft)
            throw new InvalidOperationException("Therapy plan must be in draft status to be created.");
        yield return new TherapyPlanCreated(command.Id, command.Goals, command.Description);
    }
}
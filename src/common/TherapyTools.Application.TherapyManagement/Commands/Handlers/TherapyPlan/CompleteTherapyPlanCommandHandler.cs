using TherapyTools.Application.Common.Interfaces;
using TherapyTools.Domain.Common.Interfaces;
using TherapyTools.Domain.TherapyManagement;

namespace TherapyTools.Application.TherapyManagement.Commands.Handlers.TherapyPlan;

public class CompleteTherapyPlanCommandHandler : AbstractTherapyPlanCommandHandler<CompleteTherapyPlanCommand>
{
    public CompleteTherapyPlanCommandHandler(IEventStore<TherapyPlanId> eventStore) : base(eventStore) { }

    protected override Task<CommandDispatchResult> Handle(CompleteTherapyPlanCommand command, TherapyPlanState state)
    {
        if(state.Status != TherapyPlanStatus.Active)
            throw new InvalidOperationException("Therapy plan must be in active status to be completed.");
        var domainEvents = new List<IDomainEvent> { new TherapyPlanCompleted(command.Id) };
        return Task.FromResult(new CommandDispatchResult(domainEvents, new List<IIntegrationEvent>()));
    }
}
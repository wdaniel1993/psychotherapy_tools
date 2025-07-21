using Mediator;
using TherapyTools.Application.Common;
using TherapyTools.Domain.Common.Interfaces;
using TherapyTools.Domain.TherapyManagement;

namespace TherapyTools.Application.TherapyManagement.Commands.Handlers.TherapyPlan;

public class ActivateTherapyPlanCommandHandler(IEventStore<TherapyPlanId> eventStore) : AbstractTherapyPlanCommandHandler<ActivateTherapyPlanCommand>(eventStore)
{
    protected override Task<CommandResult> Handle(ActivateTherapyPlanCommand command, TherapyPlanState state)
    {
        if(state.Status != TherapyPlanStatus.Draft)
            throw new InvalidOperationException("Therapy plan must be in draft status to be activated.");
        var domainEvents = new List<IDomainEvent> { new TherapyPlanActivated(command.Id) };
        return Task.FromResult(new CommandResult(domainEvents, new List<INotification>()));
    }
}
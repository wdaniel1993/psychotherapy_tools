using TherapyTools.Application.Common.Interfaces;
using TherapyTools.Domain.Common.Interfaces;
using TherapyTools.Domain.TherapyManagement;
using System.Threading.Tasks;
using System.Collections.Generic;

namespace TherapyTools.Application.TherapyManagement.Commands.Handlers.TherapyPlan;

public class ActivateTherapyPlanCommandHandler : AbstractTherapyPlanCommandHandler<ActivateTherapyPlanCommand>
{
    public ActivateTherapyPlanCommandHandler(IEventStore<TherapyPlanId> eventStore) : base(eventStore) { }

    protected override Task<CommandDispatchResult> Handle(ActivateTherapyPlanCommand command, TherapyPlanState state)
    {
        if(state.Status != TherapyPlanStatus.Draft)
            throw new InvalidOperationException("Therapy plan must be in draft status to be activated.");
        var domainEvents = new List<IDomainEvent> { new TherapyPlanActivated(command.Id) };
        return Task.FromResult(new CommandDispatchResult(domainEvents, new List<IIntegrationEvent>()));
    }
}
using TherapyTools.Application.Common.Interfaces;
using TherapyTools.Domain.Common.Interfaces;
using TherapyTools.Domain.TherapyManagement;
using System.Threading.Tasks;
using System.Collections.Generic;

namespace TherapyTools.Application.TherapyManagement.Commands.Handlers.TherapyPlan;

public class CreateTherapyPlanCommandHandler : AbstractTherapyPlanCommandHandler<CreateTherapyPlanCommand>
{
    public CreateTherapyPlanCommandHandler(IEventStore<TherapyPlanId> eventStore) : base(eventStore) { }

    protected override Task<CommandDispatchResult> Handle(CreateTherapyPlanCommand command, TherapyPlanState state)
    {
        if(state.Status != TherapyPlanStatus.Draft)
            throw new InvalidOperationException("Therapy plan must be in draft status to be created.");
        var domainEvents = new List<IDomainEvent> { new TherapyPlanCreated(command.Id, command.GoalList, command.Description) };
        return Task.FromResult(new CommandDispatchResult(domainEvents, new List<IIntegrationEvent>()));
    }
}
using TherapyTools.Application.Common.Interfaces;
using TherapyTools.Domain.Common.Interfaces;
using TherapyTools.Domain.TherapyManagement;

namespace TherapyTools.Application.TherapyManagement.Commands.Handlers.TherapyPlan;

public class CreateTherapyPlanCommandHandler : IAggregateCommandHandler<CreateTherapyPlanCommand, TherapyPlanId, TherapyPlanState>
{
    public Task<IEnumerable<IDomainEvent>> Handle(CreateTherapyPlanCommand command, TherapyPlanState state)
    {
        if(state.Status != TherapyPlanStatus.Draft)
            throw new InvalidOperationException("Therapy plan must be in draft status to be created.");
        return Task.FromResult<IEnumerable<IDomainEvent>>([new TherapyPlanCreated(command.Id, command.GoalList, command.Description)]);
    }
}
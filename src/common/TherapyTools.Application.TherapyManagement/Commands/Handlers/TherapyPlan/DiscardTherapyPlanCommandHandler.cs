using TherapyTools.Application.Common.Interfaces;
using TherapyTools.Domain.Common.Interfaces;
using TherapyTools.Domain.TherapyManagement;

namespace TherapyTools.Application.TherapyManagement.Commands.Handlers.TherapyPlan;

public class DiscardTherapyPlanCommandHandler : AbstractTherapyPlanCommandHandler<DiscardTherapyPlanCommand>
{
    public DiscardTherapyPlanCommandHandler(IEventStore<TherapyPlanId> eventStore) : base(eventStore) { }

    protected override Task<CommandResult> Handle(DiscardTherapyPlanCommand command, TherapyPlanState state)
    {
        if (state.Status == TherapyPlanStatus.Completed)
            throw new InvalidOperationException("Therapy plan cannot be discarded after completion.");
        var domainEvents = new List<IDomainEvent> { new TherapyPlanDiscard(command.Id) };
        return Task.FromResult(new CommandResult(domainEvents, new List<IIntegrationEvent>()));
    }
}
using TherapyTools.Application.Common.Interfaces;
using TherapyTools.Domain.Common.Interfaces;
using TherapyTools.Domain.TherapyManagement;
using System.Threading.Tasks;
using System.Collections.Generic;

namespace TherapyTools.Application.TherapyManagement.Commands.Handlers.TherapySession;

public class ScheduleTherapySessionCommandHandler : AbstractTherapySessionCommandHandler<ScheduleTherapySessionCommand>
{
    public ScheduleTherapySessionCommandHandler(IEventStore<TherapySessionId> eventStore) : base(eventStore) { }

    protected override Task<CommandDispatchResult> Handle(ScheduleTherapySessionCommand command, TherapySessionState state)
    {
        if(command.SessionTimeSlot.Start < DateTime.UtcNow)
            throw new InvalidOperationException("Cannot schedule a session in the past.");
        if (state.Status != TherapySessionStatus.Unconfirmed)
            throw new InvalidOperationException("Cannot schedule a session that is already scheduled or canceled.");
        var domainEvents = new List<IDomainEvent> { new TherapySessionScheduled(command.Id, command.SessionTimeSlot, command.Type, command.Notes) };
        return Task.FromResult(new CommandDispatchResult(domainEvents, new List<IIntegrationEvent>()));
    }
}

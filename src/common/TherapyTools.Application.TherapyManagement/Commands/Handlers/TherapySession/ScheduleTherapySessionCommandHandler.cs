using TherapyTools.Application.Common.Interfaces;
using TherapyTools.Domain.Common.Interfaces;
using TherapyTools.Domain.TherapyManagement;

namespace TherapyTools.Application.TherapyManagement.Commands.Handlers.TherapySession;

public class ScheduleTherapySessionCommandHandler : IAggregateCommandHandler<ScheduleTherapySessionCommand, TherapySessionId, TherapySessionState>
{
    public Task<IEnumerable<IDomainEvent>> Handle(ScheduleTherapySessionCommand command, TherapySessionState state)
    {
        if(command.SessionTimeSlot.Start < DateTime.UtcNow)
            throw new InvalidOperationException("Cannot schedule a session in the past.");
        if (state.Status != TherapySessionStatus.Unconfirmed)
            throw new InvalidOperationException("Cannot schedule a session that is already scheduled or canceled.");
        return Task.FromResult<IEnumerable<IDomainEvent>>([new TherapySessionScheduled(command.Id, command.SessionTimeSlot, command.Type, command.Notes)]);
    }
}

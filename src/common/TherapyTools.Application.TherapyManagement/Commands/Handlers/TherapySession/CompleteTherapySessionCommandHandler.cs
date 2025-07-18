using TherapyTools.Application.Common.Interfaces;
using TherapyTools.Domain.Common.Interfaces;
using TherapyTools.Domain.TherapyManagement;

namespace TherapyTools.Application.TherapyManagement.Commands.Handlers.TherapySession;

public class CompleteTherapySessionCommandHandler : IAggregateCommandHandler<CompleteTherapySessionCommand, TherapySessionId, TherapySessionState>
{
    public Task<IEnumerable<IDomainEvent>> Handle(CompleteTherapySessionCommand command, TherapySessionState state)
    {
        if (state.Status == TherapySessionStatus.Done)
            throw new InvalidOperationException("Cannot complete a session that is already done.");
        if (state.Status != TherapySessionStatus.Scheduled)
            throw new InvalidOperationException("Cannot complete a session that is not scheduled.");
        return Task.FromResult<IEnumerable<IDomainEvent>>([new TherapySessionCompleted(command.Id, command.Notes)]);
    }
}
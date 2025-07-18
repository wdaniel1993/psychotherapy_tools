using TherapyTools.Domain.Common.Cqrs;

namespace TherapyTools.Domain.TherapyManagement.CommandHandlers.TherapySession;

public class UpdateTherapySessionNotesCommandHandler : IAggregateCommandHandler<UpdateTherapySessionNotesCommand, TherapySessionId, TherapySessionState>
{
    public Task<IEnumerable<IDomainEvent>> Handle(UpdateTherapySessionNotesCommand command, TherapySessionState state)
    {
        if(state.Status != TherapySessionStatus.Unconfirmed)
            throw new InvalidOperationException("Cannot update notes for a session that is not unconfirmed.");
        return Task.FromResult<IEnumerable<IDomainEvent>>([new TherapySessionNotesUpdates(command.Id, command.Notes)]);
    }
}
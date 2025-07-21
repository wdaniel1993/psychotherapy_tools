using Mediator;
using TherapyTools.Application.Common;
using TherapyTools.Domain.Common.Interfaces;
using TherapyTools.Domain.TherapyManagement;

namespace TherapyTools.Application.TherapyManagement.Commands.Handlers.TherapySession;

public class UpdateTherapySessionNotesCommandHandler(IEventStore<TherapySessionId> eventStore) : AbstractTherapySessionCommandHandler<UpdateTherapySessionNotesCommand>(eventStore)
{
    protected override Task<CommandResult> Handle(UpdateTherapySessionNotesCommand command, TherapySessionState state)
    {
        if(state.Status != TherapySessionStatus.Unconfirmed)
            throw new InvalidOperationException("Cannot update notes for a session that is not unconfirmed.");
        var domainEvents = new List<IDomainEvent> { new TherapySessionNotesUpdates(command.Id, command.Notes) };
        return Task.FromResult(new CommandResult(domainEvents, new List<INotification>()));
    }
}
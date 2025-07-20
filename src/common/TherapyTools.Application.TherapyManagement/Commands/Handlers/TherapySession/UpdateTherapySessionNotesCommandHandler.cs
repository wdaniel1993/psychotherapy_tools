using TherapyTools.Application.TherapyManagement;
using TherapyTools.Application.Common.Interfaces;
using TherapyTools.Domain.Common.Interfaces;
using TherapyTools.Domain.TherapyManagement;
using System.Threading.Tasks;
using System.Collections.Generic;

namespace TherapyTools.Application.TherapyManagement.Commands.Handlers.TherapySession;

public class UpdateTherapySessionNotesCommandHandler : TherapySessionAggregateCommandHandler<UpdateTherapySessionNotesCommand>
{
    public UpdateTherapySessionNotesCommandHandler(IEventStore<TherapySessionId> eventStore) : base(eventStore) { }

    protected override Task<CommandDispatchResult> Handle(UpdateTherapySessionNotesCommand command, TherapySessionState state)
    {
        if(state.Status != TherapySessionStatus.Unconfirmed)
            throw new InvalidOperationException("Cannot update notes for a session that is not unconfirmed.");
        var domainEvents = new List<IDomainEvent> { new TherapySessionNotesUpdates(command.Id, command.Notes) };
        return Task.FromResult(new CommandDispatchResult(domainEvents, new List<IIntegrationEvent>()));
    }
}
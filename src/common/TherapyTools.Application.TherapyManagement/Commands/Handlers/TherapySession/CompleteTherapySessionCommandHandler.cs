using Mediator;
using TherapyTools.Application.Common;
using TherapyTools.Domain.Common.Interfaces;
using TherapyTools.Domain.TherapyManagement;

namespace TherapyTools.Application.TherapyManagement.Commands.Handlers.TherapySession;

public class CompleteTherapySessionCommandHandler(IEventStore<TherapySessionId> eventStore) : AbstractTherapySessionCommandHandler<CompleteTherapySessionCommand>(eventStore)
{
    protected override Task<CommandResult> Handle(CompleteTherapySessionCommand command, TherapySessionState state)
    {
        if (state.Status == TherapySessionStatus.Done)
            throw new InvalidOperationException("Cannot complete a session that is already done.");
        if (state.Status != TherapySessionStatus.Scheduled)
            throw new InvalidOperationException("Cannot complete a session that is not scheduled.");
        var domainEvents = new List<IDomainEvent> { new TherapySessionCompleted(command.Id, command.Notes) };
        return Task.FromResult(new CommandResult(domainEvents, new List<INotification>()));
    }
}
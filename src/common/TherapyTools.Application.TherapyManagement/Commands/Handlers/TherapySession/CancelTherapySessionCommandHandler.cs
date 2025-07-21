using Mediator;
using TherapyTools.Application.Common;
using TherapyTools.Domain.Common.Interfaces;
using TherapyTools.Domain.TherapyManagement;

namespace TherapyTools.Application.TherapyManagement.Commands.Handlers.TherapySession;

public class CancelTherapySessionCommandHandler(IEventStore<TherapySessionId> eventStore) : AbstractTherapySessionCommandHandler<CancelTherapySessionCommand>(eventStore)
{
    protected override Task<CommandResult> Handle(CancelTherapySessionCommand command, TherapySessionState state)
    {
        if (state.Status == TherapySessionStatus.Canceled)
            throw new InvalidOperationException("Cannot cancel a session that is already canceled.");
        if (state.Status == TherapySessionStatus.Done)
            throw new InvalidOperationException("Cannot cancel a session that is already done.");
        if (state.Status != TherapySessionStatus.Scheduled)
            throw new InvalidOperationException("Cannot cancel a session that is not scheduled.");
        var domainEvents = new List<IDomainEvent> { new TherapySessionCanceled(command.Id) };
        return Task.FromResult(new CommandResult(domainEvents, new List<INotification>()));
    }
}
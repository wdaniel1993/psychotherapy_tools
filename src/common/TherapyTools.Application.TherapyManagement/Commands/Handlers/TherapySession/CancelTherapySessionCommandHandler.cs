using TherapyTools.Application.TherapyManagement;
using TherapyTools.Application.Common.Interfaces;
using TherapyTools.Domain.Common.Interfaces;
using TherapyTools.Domain.TherapyManagement;
using System.Threading.Tasks;
using System.Collections.Generic;

namespace TherapyTools.Application.TherapyManagement.Commands.Handlers.TherapySession;

public class CancelTherapySessionCommandHandler : TherapySessionAggregateCommandHandler<CancelTherapySessionCommand>
{
    public CancelTherapySessionCommandHandler(IEventStore<TherapySessionId> eventStore) : base(eventStore) { }

    protected override Task<CommandDispatchResult> Handle(CancelTherapySessionCommand command, TherapySessionState state)
    {
        if (state.Status == TherapySessionStatus.Canceled)
            throw new InvalidOperationException("Cannot cancel a session that is already canceled.");
        if (state.Status == TherapySessionStatus.Done)
            throw new InvalidOperationException("Cannot cancel a session that is already done.");
        if (state.Status != TherapySessionStatus.Scheduled)
            throw new InvalidOperationException("Cannot cancel a session that is not scheduled.");
        var domainEvents = new List<IDomainEvent> { new TherapySessionCanceled(command.Id) };
        return Task.FromResult(new CommandDispatchResult(domainEvents, new List<IIntegrationEvent>()));
    }
}
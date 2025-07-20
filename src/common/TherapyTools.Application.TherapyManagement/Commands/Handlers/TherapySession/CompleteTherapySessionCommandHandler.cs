using TherapyTools.Application.Common.Interfaces;
using TherapyTools.Domain.Common.Interfaces;
using TherapyTools.Domain.TherapyManagement;

namespace TherapyTools.Application.TherapyManagement.Commands.Handlers.TherapySession;

public class CompleteTherapySessionCommandHandler : AbstractTherapySessionCommandHandler<CompleteTherapySessionCommand>
{
    public CompleteTherapySessionCommandHandler(IEventStore<TherapySessionId> eventStore) : base(eventStore) { }

    protected override Task<CommandDispatchResult> Handle(CompleteTherapySessionCommand command, TherapySessionState state)
    {
        if (state.Status == TherapySessionStatus.Done)
            throw new InvalidOperationException("Cannot complete a session that is already done.");
        if (state.Status != TherapySessionStatus.Scheduled)
            throw new InvalidOperationException("Cannot complete a session that is not scheduled.");
        var domainEvents = new List<IDomainEvent> { new TherapySessionCompleted(command.Id, command.Notes) };
        return Task.FromResult(new CommandDispatchResult(domainEvents, new List<IIntegrationEvent>()));
    }
}
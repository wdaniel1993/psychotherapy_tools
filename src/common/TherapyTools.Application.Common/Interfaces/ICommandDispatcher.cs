using TherapyTools.Domain.Common.Interfaces;

namespace TherapyTools.Application.Common.Interfaces;

public record CommandDispatchResult(
    IEnumerable<IDomainEvent> DomainEvents,
    IEnumerable<IIntegrationEvent> IntegrationEvents
);

public interface ICommandDispatcher
{
    Task<CommandDispatchResult> DispatchAsync<TCommand>(TCommand command) where TCommand : ICommand;
}

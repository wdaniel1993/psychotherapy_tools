using TherapyTools.Domain.Common.Interfaces;

namespace TherapyTools.Application.Common.Interfaces;

public record CommandResult(
    IEnumerable<IDomainEvent> DomainEvents,
    IEnumerable<IIntegrationEvent> IntegrationEvents
);

public interface ICommandDispatcher
{
    Task<CommandResult> DispatchAsync<TCommand>(TCommand command) where TCommand : ICommand;
}

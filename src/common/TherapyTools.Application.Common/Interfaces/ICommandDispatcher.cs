using TherapyTools.Domain.Common.Interfaces;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace TherapyTools.Application.Common.Interfaces;

public record CommandDispatchResult(
    IEnumerable<IDomainEvent> DomainEvents,
    IEnumerable<IIntegrationEvent> IntegrationEvents
    // You can add more side effect collections here if needed
);

public interface ICommandDispatcher
{
    /// <summary>
    /// Dispatches a command and returns all transactional side effects (domain events, integration events, etc.).
    /// </summary>
    /// <typeparam name="TCommand">The command type.</typeparam>
    /// <param name="command">The command instance.</param>
    /// <returns>A result containing all side effects to be handled transactionally.</returns>
    Task<CommandDispatchResult> DispatchAsync<TCommand>(TCommand command) where TCommand : ICommand;
}

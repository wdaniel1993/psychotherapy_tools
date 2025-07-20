using TherapyTools.Domain.Common.Interfaces;

namespace TherapyTools.Application.Common.Interfaces;

public record CommandResult(
    IEnumerable<IDomainEvent> DomainEvents,
    IEnumerable<IIntegrationEvent> IntegrationEvents
);

public interface ICommandHandler<TCommand> where TCommand : ICommand
{
    Task<CommandResult> Handle(TCommand command);
}
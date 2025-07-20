using TherapyTools.Domain.Common.Interfaces;

namespace TherapyTools.Application.Common.Interfaces;

public interface ICommandDispatcher
{
    Task<CommandResult> DispatchAsync<TCommand>(TCommand command) where TCommand : ICommand;
}
using System.Collections.Concurrent;
using Microsoft.Extensions.DependencyInjection;
using TherapyTools.Application.Common.Interfaces;

namespace TherapyTools.Infrastructure.InProcess;

public class InProcessCommandDispatcher(IServiceProvider serviceProvider) : ICommandDispatcher
{
    private readonly ConcurrentBag<object> _commands = new();

    public async Task<CommandResult> DispatchAsync<TCommand>(TCommand command) where TCommand : ICommand
    {
        _commands.Add(command!);
        var handler = serviceProvider.GetService<ICommandHandler<TCommand>>();
        if (handler == null)
            throw new InvalidOperationException($"No handler registered for command type {typeof(TCommand).Name}");
        return await handler.Handle(command);
    }

    // For testing or inspection
    public IEnumerable<object> GetDispatchedCommands() => _commands.ToArray();
}

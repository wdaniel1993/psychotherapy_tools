using System.Collections.Concurrent;
using TherapyTools.Application.Common.Interfaces;

namespace TherapyTools.Infrastructure.InProcess;

public class InProcessIntegrationEventDispatcher : IIntegrationEventDispatcher
{
    private readonly ConcurrentBag<IIntegrationEvent> _events = new();

    public Task DispatchAsync(IIntegrationEvent integrationEvent)
    {
        _events.Add(integrationEvent);
        return Task.CompletedTask;
    }

    public IEnumerable<IIntegrationEvent> GetDispatchedEvents() => _events.ToArray();
}

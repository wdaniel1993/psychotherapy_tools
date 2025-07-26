using Mediator;
using System.Collections.Concurrent;

namespace TherapyTools.Infrastructure.InProcess;

public class InProcessNotificationStore
{
    private readonly ConcurrentBag<INotification> _notifications = new();

    public void Add(INotification notification)
    {
        _notifications.Add(notification);
    }

    public IEnumerable<INotification> GetAll() => _notifications;
}

public class InProcessNotificationHandler : INotificationHandler<INotification>
{
    private readonly InProcessNotificationStore _store;

    public InProcessNotificationHandler(InProcessNotificationStore store)
    {
        _store = store;
    }

    public ValueTask Handle(INotification notification, CancellationToken cancellationToken)
    {
        _store.Add(notification);
        return ValueTask.CompletedTask;
    }
}

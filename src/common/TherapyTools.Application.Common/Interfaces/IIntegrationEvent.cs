using Mediator;

namespace TherapyTools.Application.Common.Interfaces;

public interface IIntegrationEvent : INotification
{
    string EventName { get; }
    IntegrationEventType EventType { get; }
}
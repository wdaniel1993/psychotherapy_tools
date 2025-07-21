using Mediator;
using TherapyTools.Domain.Common.Interfaces;

namespace TherapyTools.Application.Common.Interfaces;

public interface IIntegrationEvent : INotification
{
    string EventName { get; }
    IntegrationEventType EventType { get; }
}
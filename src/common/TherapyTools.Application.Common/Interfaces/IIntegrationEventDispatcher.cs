namespace TherapyTools.Application.Common.Interfaces;

public interface IIntegrationEventDispatcher
{
    Task DispatchAsync(IIntegrationEvent integrationEvent);
}
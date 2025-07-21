using TherapyTools.Domain.Common.Interfaces;
using Mediator;

namespace TherapyTools.Application.Common.Interfaces;

public record CommandResult(
    IEnumerable<IDomainEvent> DomainEvents,
    IEnumerable<INotification> IntegrationEvents
);
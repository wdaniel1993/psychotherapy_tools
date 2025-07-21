using TherapyTools.Domain.Common.Interfaces;

namespace TherapyTools.Application.Common.Interfaces;

public interface IAggregateIntegrationEvent<TAggregateId> : IIntegrationEvent
    where TAggregateId : IAggregateId
{
    TAggregateId AggregateId { get; }
}
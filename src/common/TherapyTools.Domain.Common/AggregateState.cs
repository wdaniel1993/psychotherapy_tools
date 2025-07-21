using TherapyTools.Domain.Common.Interfaces;

namespace TherapyTools.Domain.Common;

public abstract record AggregateState<TAggregateId>(TAggregateId AggregateId)
    where TAggregateId : IAggregateId;
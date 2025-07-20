using TherapyTools.Application.Common.Interfaces;
using TherapyTools.Domain.Common.Interfaces;

namespace TherapyTools.Application.Common;

public abstract record AggregateCommand<TAggregateId>(TAggregateId Id) : IAggregateCommand<TAggregateId>
    where TAggregateId : IAggregateId
{
    public TAggregateId AggregateId => Id;
}
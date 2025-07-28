using TherapyTools.Application.Common.Interfaces;

namespace TherapyTools.Application.Common;

public abstract record AggregateCommand(Guid Id) : IAggregateCommand
{
    public Guid AggregateId => Id;
}
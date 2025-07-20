using TherapyTools.Domain.Common.Interfaces;

namespace TherapyTools.Application.Common.Interfaces;

public interface ICommand {}
public interface IAggregateCommand<TAggregateId> : ICommand
    where TAggregateId : IAggregateId
{ 
    TAggregateId AggregateId { get; }
}
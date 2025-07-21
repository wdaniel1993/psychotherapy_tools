using Mediator;
using TherapyTools.Domain.Common.Interfaces;

namespace TherapyTools.Application.Common.Interfaces;

public interface IAggregateCommand<TAggregateId> : ICommand<CommandResult>
    where TAggregateId : IAggregateId
{ 
    TAggregateId AggregateId { get; }
}
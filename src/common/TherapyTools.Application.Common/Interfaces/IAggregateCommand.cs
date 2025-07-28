using Mediator;

namespace TherapyTools.Application.Common.Interfaces;

public interface IAggregateCommand : ICommand<CommandResult>
{ 
    Guid AggregateId { get; }
}
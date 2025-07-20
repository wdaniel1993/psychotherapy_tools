namespace TherapyTools.Application.Common.Interfaces;

// Updated to return CommandDispatchResult for transactional side effects
public interface ICommandHandler<TCommand> where TCommand : ICommand
{
    Task<CommandDispatchResult> Handle(TCommand command);
}
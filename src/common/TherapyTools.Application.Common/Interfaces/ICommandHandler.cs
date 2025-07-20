namespace TherapyTools.Application.Common.Interfaces;

// Updated to return CommandResult for transactional side effects
public interface ICommandHandler<TCommand> where TCommand : ICommand
{
    Task<CommandResult> Handle(TCommand command);
}
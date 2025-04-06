namespace TherapyTools.Domain.Common.Cqrs;
public interface ICommandHandler<TCommand> where TCommand : IDomainCommand
{
    IAsyncEnumerable<IDomainEvent> Handle(TCommand command);
}
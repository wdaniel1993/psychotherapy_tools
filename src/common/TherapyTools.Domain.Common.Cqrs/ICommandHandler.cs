namespace TherapyTools.Domain.Common.Cqrs;
public interface ICommandHandler<TCommand> where TCommand : IDomainCommand
{
    IEnumerable<IDomainEvent> Handle(TCommand command);
}
namespace TherapyTools.Domain.Common.Cqrs;
public interface ICommandHandler<TCommand>
{
    IEnumerable<IDomainEvent> Handle(TCommand command);
}
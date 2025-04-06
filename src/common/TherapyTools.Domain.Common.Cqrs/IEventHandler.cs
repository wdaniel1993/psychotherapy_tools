namespace TherapyTools.Domain.Common.Cqrs;

public interface IEventHandler<TEvent> where TEvent : IDomainEvent
{
    void Handle(TEvent @event);
}
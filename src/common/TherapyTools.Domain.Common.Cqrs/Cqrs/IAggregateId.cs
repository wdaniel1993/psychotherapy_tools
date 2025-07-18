namespace TherapyTools.Domain.Common.Cqrs;

public interface IAggregateId
{
    Guid ToGuid();
}
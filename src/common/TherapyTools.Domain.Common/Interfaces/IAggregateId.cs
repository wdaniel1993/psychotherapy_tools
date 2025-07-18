namespace TherapyTools.Domain.Common.Interfaces;

public interface IAggregateId
{
    Guid ToGuid();
}
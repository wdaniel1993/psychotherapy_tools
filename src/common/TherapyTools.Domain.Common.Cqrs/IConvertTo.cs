namespace TherapyTools.Domain.Common.Cqrs;

public interface IConvertTo<TType>
{
    TType To();
}
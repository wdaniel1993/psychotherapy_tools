using System.Threading;
using System.Threading.Tasks;

namespace TherapyTools.Application.Common.Interfaces;

public interface IQueryHandler<TQuery, TResult> where TQuery : IQuery<TResult>
{
    Task<TResult> Handle(TQuery query, CancellationToken cancellationToken);
}

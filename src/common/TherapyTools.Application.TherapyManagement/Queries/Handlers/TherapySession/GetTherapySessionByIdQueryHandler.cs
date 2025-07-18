using TherapyTools.Domain.TherapyManagement;
using TherapyTools.Application.Common.Interfaces;
using TherapyTools.Domain.Common.Interfaces;

namespace TherapyTools.Application.TherapyManagement.Queries.Handlers.TherapySession;

public class GetTherapySessionByIdQueryHandler(IEventStore<TherapySessionId> eventStore) : IQueryHandler<GetTherapySessionByIdQuery, TherapySessionState>
{
    public async Task<TherapySessionState> Handle(GetTherapySessionByIdQuery query, CancellationToken cancellationToken)
    {
        return await TherapySessionAggregate.GetCurrentState(eventStore, query.Id);
    }
}

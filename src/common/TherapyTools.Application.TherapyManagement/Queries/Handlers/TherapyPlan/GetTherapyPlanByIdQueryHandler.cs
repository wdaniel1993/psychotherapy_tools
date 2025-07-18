using TherapyTools.Domain.TherapyManagement;
using TherapyTools.Application.Common.Interfaces;
using TherapyTools.Domain.Common.Interfaces;

namespace TherapyTools.Application.TherapyManagement.Queries.Handlers.TherapyPlan;

public class GetTherapyPlanByIdQueryHandler(IEventStore<TherapyPlanId> eventStore)
    : IQueryHandler<GetTherapyPlanByIdQuery, TherapyPlanState>
{
    public async Task<TherapyPlanState> Handle(GetTherapyPlanByIdQuery query, CancellationToken cancellationToken)
    {
        return await TherapyPlanAggregate.GetCurrentState(eventStore, query.Id);
    }
}

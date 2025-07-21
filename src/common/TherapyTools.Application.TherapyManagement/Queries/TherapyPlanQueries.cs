using Mediator;
using TherapyTools.Domain.TherapyManagement;

namespace TherapyTools.Application.TherapyManagement.Queries;

public record GetTherapyPlanByIdQuery(TherapyPlanId Id) : IQuery<TherapyPlanState>;

using TherapyTools.Domain.TherapyManagement;
using TherapyTools.Application.Common.Interfaces;

namespace TherapyTools.Application.TherapyManagement.Queries;

public record GetTherapyPlanByIdQuery(TherapyPlanId Id) : IQuery<TherapyPlanState>;

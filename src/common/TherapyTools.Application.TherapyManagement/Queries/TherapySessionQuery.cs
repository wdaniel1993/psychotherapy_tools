using Mediator;
using TherapyTools.Domain.TherapyManagement;

namespace TherapyTools.Application.TherapyManagement.Queries;

public record GetTherapySessionByIdQuery(TherapySessionId Id) : IQuery<TherapySessionState>;
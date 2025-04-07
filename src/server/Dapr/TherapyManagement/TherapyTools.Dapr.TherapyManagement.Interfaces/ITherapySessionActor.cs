using Dapr.Actors;
using TherapyTools.Domain.TherapyManagement;

namespace TherapyTools.Dapr.TherapyManagement.Actors.Interfaces;

public interface ITherapySessionActor : IActor
{
    Task ExecuteCommand(ITherapySessionCommand therapySessionCommand);
}

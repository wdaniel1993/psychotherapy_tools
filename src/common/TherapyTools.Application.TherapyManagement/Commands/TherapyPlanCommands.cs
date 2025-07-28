using TherapyTools.Application.Common;
using TherapyTools.Application.TherapyManagement.DataAccess;

namespace TherapyTools.Application.TherapyManagement.Commands;

public record CreateTherapyPlanCommand(Guid Id, List<GoalModel> GoalList, string Description) : AggregateCommand(Id); 
public record ActivateTherapyPlanCommand(Guid Id) : AggregateCommand(Id);
public record CompleteTherapyPlanCommand(Guid Id) : AggregateCommand(Id);
public record DiscardTherapyPlanCommand(Guid Id) : AggregateCommand(Id);
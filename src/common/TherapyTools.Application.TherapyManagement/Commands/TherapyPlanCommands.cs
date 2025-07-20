using TherapyTools.Application.Common;
using TherapyTools.Application.Common.Interfaces;
using TherapyTools.Domain.TherapyManagement;

namespace TherapyTools.Application.TherapyManagement.Commands;

public record CreateTherapyPlanCommand(TherapyPlanId Id, GoalList GoalList, TherapyPlanDescription Description) : AggregateCommand<TherapyPlanId>(Id); 
public record ActivateTherapyPlanCommand(TherapyPlanId Id) : AggregateCommand<TherapyPlanId>(Id);
public record CompleteTherapyPlanCommand(TherapyPlanId Id) : AggregateCommand<TherapyPlanId>(Id);
public record DiscardTherapyPlanCommand(TherapyPlanId Id) : AggregateCommand<TherapyPlanId>(Id);
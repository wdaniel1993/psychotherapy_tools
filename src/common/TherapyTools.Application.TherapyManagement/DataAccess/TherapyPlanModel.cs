using System;
using System.Collections.Generic;
using TherapyTools.Domain.TherapyManagement;

public record TherapyPlanModel(
    Guid AggregateId,
    List<GoalModel> GoalList,
    string Description,
    TherapyPlanStatus Status
);

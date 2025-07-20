using FluentValidation;
using TherapyTools.Application.TherapyManagement.Commands;
using TherapyTools.Domain.TherapyManagement;

namespace TherapyTools.Application.TherapyManagement.Commands.Validators;

public class CreateTherapyPlanCommandValidator : AbstractValidator<CreateTherapyPlanCommand>
{
    public CreateTherapyPlanCommandValidator()
    {
        RuleFor(x => x.Id).NotNull();
        RuleFor(x => x.GoalList.Goals).NotNull().NotEmpty();
        RuleForEach(x => x.GoalList.Goals)
            .ChildRules(goal =>
            {
                goal.RuleFor(g => g.Title).NotEmpty();
                goal.RuleFor(g => g.Description).NotEmpty();
            });
        RuleFor(x => x.Description.Content).NotEmpty();
    }
}

public class ActivateTherapyPlanCommandValidator : AbstractValidator<ActivateTherapyPlanCommand>
{
    public ActivateTherapyPlanCommandValidator()
    {
        RuleFor(x => x.Id).NotNull();
    }
}

public class CompleteTherapyPlanCommandValidator : AbstractValidator<CompleteTherapyPlanCommand>
{
    public CompleteTherapyPlanCommandValidator()
    {
        RuleFor(x => x.Id).NotNull();
    }
}

public class DiscardTherapyPlanCommandValidator : AbstractValidator<DiscardTherapyPlanCommand>
{
    public DiscardTherapyPlanCommandValidator()
    {
        RuleFor(x => x.Id).NotNull();
    }
}

using FluentValidation;

namespace TherapyTools.Application.TherapyManagement.Commands.Validators;

public class CreateTherapyPlanCommandValidator : AbstractValidator<CreateTherapyPlanCommand>
{
    public CreateTherapyPlanCommandValidator()
    {
        RuleFor(x => x.Id).NotEmpty();
        RuleFor(x => x.GoalList).NotNull().NotEmpty();
        RuleForEach(x => x.GoalList)
            .ChildRules(goal =>
            {
                goal.RuleFor(g => g.Title).NotEmpty();
                goal.RuleFor(g => g.Description).NotEmpty();
            });
        RuleFor(x => x.Description).NotEmpty();
    }
}

public class ActivateTherapyPlanCommandValidator : AbstractValidator<ActivateTherapyPlanCommand>
{
    public ActivateTherapyPlanCommandValidator()
    {
        RuleFor(x => x.Id).NotEmpty();
    }
}

public class CompleteTherapyPlanCommandValidator : AbstractValidator<CompleteTherapyPlanCommand>
{
    public CompleteTherapyPlanCommandValidator()
    {
        RuleFor(x => x.Id).NotEmpty();
    }
}

public class DiscardTherapyPlanCommandValidator : AbstractValidator<DiscardTherapyPlanCommand>
{
    public DiscardTherapyPlanCommandValidator()
    {
        RuleFor(x => x.Id).NotEmpty();
    }
}

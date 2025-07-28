using FluentValidation;

namespace TherapyTools.Application.TherapyManagement.Commands.Validators;

public class ScheduleTherapySessionCommandValidator : AbstractValidator<ScheduleTherapySessionCommand>
{
    public ScheduleTherapySessionCommandValidator()
    {
        RuleFor(x => x.Id).NotEmpty();
        RuleFor(x => x.SessionTimeSlot)
            .NotNull()
            .Must(slot => slot.Start < slot.End)
            .WithMessage("Session start must be before end.");
        RuleFor(x => x.Type).IsInEnum();
        RuleFor(x => x.Notes).NotEmpty();
    }
}

public class RescheduleTherapySessionCommandValidator : AbstractValidator<RescheduleTherapySessionCommand>
{
    public RescheduleTherapySessionCommandValidator()
    {
        RuleFor(x => x.Id).NotEmpty();
        RuleFor(x => x.NewSlot)
            .NotNull()
            .Must(slot => slot.Start < slot.End)
            .WithMessage("Session start must be before end.");
    }
}

public class CancelTherapySessionCommandValidator : AbstractValidator<CancelTherapySessionCommand>
{
    public CancelTherapySessionCommandValidator()
    {
        RuleFor(x => x.Id).NotEmpty();
    }
}

public class CompleteTherapySessionCommandValidator : AbstractValidator<CompleteTherapySessionCommand>
{
    public CompleteTherapySessionCommandValidator()
    {
        RuleFor(x => x.Id).NotEmpty();
        RuleFor(x => x.Notes).NotEmpty();
    }
}

public class UpdateTherapySessionNotesCommandValidator : AbstractValidator<UpdateTherapySessionNotesCommand>
{
    public UpdateTherapySessionNotesCommandValidator()
    {
        RuleFor(x => x.Id).NotEmpty();
        RuleFor(x => x.Notes).NotEmpty();
    }
}

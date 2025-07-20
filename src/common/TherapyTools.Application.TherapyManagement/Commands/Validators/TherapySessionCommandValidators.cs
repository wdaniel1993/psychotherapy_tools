using FluentValidation;
using TherapyTools.Application.TherapyManagement.Commands;
using TherapyTools.Domain.TherapyManagement;
using TherapyTools.Domain.TherapyManagement.ValueObjects;

namespace TherapyTools.Application.TherapyManagement.Commands.Validators;

public class ScheduleTherapySessionCommandValidator : AbstractValidator<ScheduleTherapySessionCommand>
{
    public ScheduleTherapySessionCommandValidator()
    {
        RuleFor(x => x.Id).NotNull();
        RuleFor(x => x.SessionTimeSlot)
            .NotNull()
            .Must(slot => slot.Start < slot.End)
            .WithMessage("Session start must be before end.");
        RuleFor(x => x.Type).IsInEnum();
        RuleFor(x => x.Notes.Content).NotEmpty();
    }
}

public class RescheduleTherapySessionCommandValidator : AbstractValidator<RescheduleTherapySessionCommand>
{
    public RescheduleTherapySessionCommandValidator()
    {
        RuleFor(x => x.Id).NotNull();
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
        RuleFor(x => x.Id).NotNull();
    }
}

public class CompleteTherapySessionCommandValidator : AbstractValidator<CompleteTherapySessionCommand>
{
    public CompleteTherapySessionCommandValidator()
    {
        RuleFor(x => x.Id).NotNull();
        RuleFor(x => x.Notes.Content).NotEmpty();
    }
}

public class UpdateTherapySessionNotesCommandValidator : AbstractValidator<UpdateTherapySessionNotesCommand>
{
    public UpdateTherapySessionNotesCommandValidator()
    {
        RuleFor(x => x.Id).NotNull();
        RuleFor(x => x.Notes.Content).NotEmpty();
    }
}

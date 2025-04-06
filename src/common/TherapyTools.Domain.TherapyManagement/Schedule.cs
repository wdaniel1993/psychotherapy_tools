using TherapyTools.Domain.Common.Cqrs;

namespace TherapyTools.Domain.TherapyManagement;

public record TimeSlot(DateTime StartDate, DateTime EndDate);

public record Schedule
{
    public Guid Id { get; init; }
    public required List<TimeSlot> Appointments { get; init; }

    public Schedule AddAppointment(TimeSlot appointment) =>
        this with { Appointments = [.. Appointments, appointment] };

    public Schedule RemoveAppointment(TimeSlot appointment) =>
        this with { Appointments = new List<TimeSlot>(Appointments).FindAll(a => a != appointment) };
}

public record AppointmentAdded(Guid ScheduleId, TimeSlot Appointment) : IDomainEvent;
public record AppointmentRemoved(Guid ScheduleId, TimeSlot Appointment) : IDomainEvent;

public record AddAppointmentCommand(Guid ScheduleId, TimeSlot Appointment);
public record RemoveAppointmentCommand(Guid ScheduleId, TimeSlot Appointment);

public class ScheduleCommandHandler
{
    public static IEnumerable<IDomainEvent> Handle(AddAppointmentCommand command)
    {
        yield return new AppointmentAdded(command.ScheduleId, command.Appointment);
    }

    public static IEnumerable<IDomainEvent> Handle(RemoveAppointmentCommand command)
    {
        yield return new AppointmentRemoved(command.ScheduleId, command.Appointment);
    }
}

public class ScheduleEventHandler
{
    public static Schedule Apply(AppointmentAdded @event, Schedule schedule) =>
        schedule.AddAppointment(@event.Appointment);

    public static Schedule Apply(AppointmentRemoved @event, Schedule schedule) =>
        schedule.RemoveAppointment(@event.Appointment);
}
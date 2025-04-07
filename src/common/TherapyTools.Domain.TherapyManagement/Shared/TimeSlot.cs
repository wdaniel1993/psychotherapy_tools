namespace TherapyTools.Domain.TherapyManagement.Shared;

public record struct TimeSlot{

    public TimeSlot(DateTime start, DateTime end)
    {
        if (end <= start)
            throw new ArgumentException("Start time must be before end time.");
        
        Start = start;
        End = end;
    }

    public DateTime Start { get; }
    public DateTime End { get; }
}

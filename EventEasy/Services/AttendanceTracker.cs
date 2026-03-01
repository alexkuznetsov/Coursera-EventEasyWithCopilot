namespace EventEasy.Services;

public class AttendanceTracker
{
    private readonly Dictionary<int, AttendanceStats> _eventAttendance = [];

    public AttendanceStats GetStats(int eventId)
    {
        return _eventAttendance.TryGetValue(eventId, out var stats)
            ? stats
            : new AttendanceStats();
    }

    public IReadOnlyDictionary<int, AttendanceStats> GetAllStats()
    {
        return _eventAttendance;
    }

    public void RecordRegistration(int eventId, int tickets)
    {
        if (!_eventAttendance.TryGetValue(eventId, out var stats))
        {
            stats = new AttendanceStats();
            _eventAttendance[eventId] = stats;
        }

        stats.Participants += 1;
        stats.TicketsReserved += tickets;
    }
}

public class AttendanceStats
{
    public int Participants { get; set; }

    public int TicketsReserved { get; set; }
}
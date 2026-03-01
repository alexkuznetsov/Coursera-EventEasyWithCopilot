namespace EventEasy.Services;

public class UserSessionTracker
{
    private readonly HashSet<int> _registeredEventIds = [];

    public event Action? StateChanged;

    public string SessionId { get; private set; } = Guid.NewGuid().ToString("N");

    public DateTime StartedAtUtc { get; private set; } = DateTime.UtcNow;

    public DateTime LastActivityUtc { get; private set; } = DateTime.UtcNow;

    public string LastVisitedRoute { get; private set; } = "/events";

    public int RegistrationCount { get; private set; }

    public string? LastRegistrantName { get; private set; }

    public string? LastRegistrantEmail { get; private set; }

    public IReadOnlyCollection<int> RegisteredEventIds => _registeredEventIds;

    public void TrackPageView(string route)
    {
        LastVisitedRoute = route;
        LastActivityUtc = DateTime.UtcNow;
        StateChanged?.Invoke();
    }

    public void TrackRegistration(int eventId, string fullName, string email)
    {
        LastRegistrantName = fullName;
        LastRegistrantEmail = email;
        RegistrationCount++;
        _registeredEventIds.Add(eventId);
        LastActivityUtc = DateTime.UtcNow;
        StateChanged?.Invoke();
    }

    public UserSessionSnapshot CreateSnapshot()
    {
        return new UserSessionSnapshot
        {
            SessionId = SessionId,
            StartedAtUtc = StartedAtUtc,
            LastActivityUtc = LastActivityUtc,
            LastVisitedRoute = LastVisitedRoute,
            RegistrationCount = RegistrationCount,
            LastRegistrantName = LastRegistrantName,
            LastRegistrantEmail = LastRegistrantEmail,
            RegisteredEventIds = [.. _registeredEventIds]
        };
    }

    public void Restore(UserSessionSnapshot snapshot)
    {
        SessionId = string.IsNullOrWhiteSpace(snapshot.SessionId) ? Guid.NewGuid().ToString("N") : snapshot.SessionId;
        StartedAtUtc = snapshot.StartedAtUtc == default ? DateTime.UtcNow : snapshot.StartedAtUtc;
        LastActivityUtc = snapshot.LastActivityUtc == default ? DateTime.UtcNow : snapshot.LastActivityUtc;
        LastVisitedRoute = string.IsNullOrWhiteSpace(snapshot.LastVisitedRoute) ? "/events" : snapshot.LastVisitedRoute;
        RegistrationCount = snapshot.RegistrationCount;
        LastRegistrantName = snapshot.LastRegistrantName;
        LastRegistrantEmail = snapshot.LastRegistrantEmail;

        _registeredEventIds.Clear();
        foreach (var eventId in snapshot.RegisteredEventIds.Distinct())
        {
            _registeredEventIds.Add(eventId);
        }
    }
}

public class UserSessionSnapshot
{
    public string SessionId { get; set; } = string.Empty;

    public DateTime StartedAtUtc { get; set; }

    public DateTime LastActivityUtc { get; set; }

    public string LastVisitedRoute { get; set; } = "/events";

    public int RegistrationCount { get; set; }

    public string? LastRegistrantName { get; set; }

    public string? LastRegistrantEmail { get; set; }

    public List<int> RegisteredEventIds { get; set; } = [];
}
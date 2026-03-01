using EventEasy.Services;

namespace EventEasy.Tests;

public class StateTrackerTests
{
    [Fact]
    public void UserSessionTracker_TracksRegistrationsAndVisitedRoutes()
    {
        var tracker = new UserSessionTracker();

        tracker.TrackPageView("/events");
        tracker.TrackRegistration(2, "Alex", "alex@example.com");

        Assert.Equal("/events", tracker.LastVisitedRoute);
        Assert.Equal(1, tracker.RegistrationCount);
        Assert.Contains(2, tracker.RegisteredEventIds);
    }

    [Fact]
    public void UserSessionTracker_CreatesAndRestoresSnapshot()
    {
        var source = new UserSessionTracker();
        source.TrackPageView("/events/2/register");
        source.TrackRegistration(2, "Alex", "alex@example.com");
        source.TrackRegistration(5, "Alex", "alex@example.com");

        var snapshot = source.CreateSnapshot();

        var restored = new UserSessionTracker();
        restored.Restore(snapshot);

        Assert.Equal(snapshot.SessionId, restored.SessionId);
        Assert.Equal("/events/2/register", restored.LastVisitedRoute);
        Assert.Equal(2, restored.RegistrationCount);
        Assert.Contains(2, restored.RegisteredEventIds);
        Assert.Contains(5, restored.RegisteredEventIds);
    }

    [Fact]
    public void AttendanceTracker_AggregatesParticipantsAndTickets()
    {
        var tracker = new AttendanceTracker();

        tracker.RecordRegistration(3, 2);
        tracker.RecordRegistration(3, 1);

        var stats = tracker.GetStats(3);
        Assert.Equal(2, stats.Participants);
        Assert.Equal(3, stats.TicketsReserved);
    }
}

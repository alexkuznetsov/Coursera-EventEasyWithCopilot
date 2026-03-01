using Bunit;
using EventEasy.Components;
using EventEasy.Models;

namespace EventEasy.Tests;

public class EventCardTests : TestContext
{
    [Fact]
    public void EventCard_DisplaysEventFieldsAndActionLinks()
    {
        var model = new EventModel
        {
            Id = 99,
            Name = "Architecture Meetup",
            Date = new DateTime(2026, 7, 20),
            Location = "Boston, MA"
        };

        var cut = RenderComponent<EventCard>(parameters => parameters
            .Add(p => p.Event, model));

        Assert.Contains("Architecture Meetup", cut.Markup);
        Assert.Contains("Boston, MA", cut.Markup);
        Assert.Contains("/events/99", cut.Markup);
        Assert.Contains("/events/99/register", cut.Markup);
    }

    [Fact]
    public void EventCard_DisablesSave_WhenInvalidAndEnablesWhenValid()
    {
        var model = new EventModel
        {
            Id = 1,
            Name = "Demo Event",
            Date = new DateTime(2026, 7, 20),
            Location = "Austin, TX"
        };

        var cut = RenderComponent<EventCard>(parameters => parameters
            .Add(p => p.Event, model)
            .Add(p => p.IsEditable, true));

        cut.FindAll("input")[0].Change(string.Empty);

        var saveButton = cut.Find("button[type=submit]");
        Assert.True(saveButton.HasAttribute("disabled"));

        cut.FindAll("input")[0].Change("Updated Event");
        saveButton = cut.Find("button[type=submit]");
        Assert.False(saveButton.HasAttribute("disabled"));
    }
}

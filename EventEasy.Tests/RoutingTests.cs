using Bunit;
using Bunit.JSInterop;
using Bunit.TestDoubles;
using EventEasy.Services;
using Microsoft.AspNetCore.Components;
using Microsoft.Extensions.DependencyInjection;

namespace EventEasy.Tests;

public class RoutingTests : TestContext
{
    public RoutingTests()
    {
        Services.AddScoped<UserSessionTracker>();
        Services.AddScoped<AttendanceTracker>();
        JSInterop.Mode = JSRuntimeMode.Loose;
    }

    [Fact]
    public void Router_NavigatesToEventListAndDetailsPages()
    {
        var cut = RenderComponent<App>();
        var nav = Services.GetRequiredService<FakeNavigationManager>();

        nav.NavigateTo("/events");
        Assert.Contains("Event List", cut.Markup);

        nav.NavigateTo("/events/1");
        Assert.Contains("Event Details", cut.Markup);
        Assert.Contains("Edit event data using two-way binding", cut.Markup);

        nav.NavigateTo("/events/new");
        Assert.Contains("Create Event", cut.Markup);

        nav.NavigateTo("/attendance");
        Assert.Contains("Attendance Tracker", cut.Markup);
    }

    [Fact]
    public void Router_ShowsNotFound_ForUnknownRoute()
    {
        var cut = RenderComponent<App>();
        var nav = Services.GetRequiredService<FakeNavigationManager>();

        nav.NavigateTo("/does-not-exist");

        Assert.Contains("Not Found", cut.Markup);
        Assert.Contains("does not exist", cut.Markup);
    }
}

using Bunit;
using EventEasy.Pages;
using EventEasy.Services;
using Microsoft.Extensions.DependencyInjection;

namespace EventEasy.Tests;

public class RegistrationFormTests : TestContext
{
    public RegistrationFormTests()
    {
        Services.AddScoped<UserSessionTracker>();
        Services.AddScoped<AttendanceTracker>();
    }

    [Fact]
    public void Registration_ShowsValidationErrors_WhenFormIsInvalid()
    {
        var cut = RenderComponent<EventRegistration>(parameters => parameters
            .Add(p => p.Id, 1));

        var submitButton = cut.Find("button[type=submit]");
        Assert.True(submitButton.HasAttribute("disabled"));

        cut.Find("form").Submit();

        var hasErrors = cut.FindAll(".validation-errors li").Count > 0;
        Assert.True(hasErrors);
    }

    [Fact]
    public void Registration_ShowsSuccessMessage_WhenFormIsValid()
    {
        var cut = RenderComponent<EventRegistration>(parameters => parameters
            .Add(p => p.Id, 1));

        cut.Find("input").Change("Alex Johnson");
        cut.FindAll("input")[1].Change("alex@example.com");
        cut.Find("input[type=number]").Change("2");

        var submitButton = cut.Find("button[type=submit]");
        Assert.False(submitButton.HasAttribute("disabled"));

        cut.Find("form").Submit();

        Assert.Contains("Registration completed for Alex Johnson.", cut.Markup);

        var session = Services.GetRequiredService<UserSessionTracker>();
        var attendance = Services.GetRequiredService<AttendanceTracker>();

        Assert.Equal(1, session.RegistrationCount);
        Assert.Equal(1, attendance.GetStats(1).Participants);
    }
}

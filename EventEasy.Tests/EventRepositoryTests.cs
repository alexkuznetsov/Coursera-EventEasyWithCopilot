using EventEasy.Data;
using EventEasy.Models;

namespace EventEasy.Tests;

public class EventRepositoryTests
{
    [Fact]
    public void EventRepository_Create_AddsNewEvent()
    {
        var created = EventRepository.Create(new EventModel
        {
            Name = "Integration Event",
            Date = new DateTime(2026, 9, 1),
            Location = "Seattle, WA"
        });

        Assert.True(created.Id > 0);
        Assert.NotNull(EventRepository.GetById(created.Id));

        EventRepository.Delete(created.Id);
    }

    [Fact]
    public void EventRepository_Update_UpdatesExistingEvent()
    {
        var created = EventRepository.Create(new EventModel
        {
            Name = "Temp Event",
            Date = new DateTime(2026, 10, 1),
            Location = "Austin, TX"
        });

        created.Name = "Temp Event Updated";
        created.Location = "Dallas, TX";

        var updated = EventRepository.Update(created);

        Assert.True(updated);
        var stored = EventRepository.GetById(created.Id);
        Assert.NotNull(stored);
        Assert.Equal("Temp Event Updated", stored!.Name);
        Assert.Equal("Dallas, TX", stored.Location);

        EventRepository.Delete(created.Id);
    }

    [Fact]
    public void EventRepository_Delete_RemovesEvent()
    {
        var created = EventRepository.Create(new EventModel
        {
            Name = "Delete Me",
            Date = new DateTime(2026, 11, 1),
            Location = "Denver, CO"
        });

        var deleted = EventRepository.Delete(created.Id);

        Assert.True(deleted);
        Assert.Null(EventRepository.GetById(created.Id));
    }
}

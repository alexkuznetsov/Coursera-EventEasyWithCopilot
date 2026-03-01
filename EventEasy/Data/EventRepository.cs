using EventEasy.Models;

namespace EventEasy.Data;

public static class EventRepository
{
    public static List<EventModel> Events { get; } =
    [
        new EventModel
        {
            Id = 1,
            Name = "Tech Summit 2026",
            Date = new DateTime(2026, 4, 15),
            Location = "New York, NY"
        },
        new EventModel
        {
            Id = 2,
            Name = "Leadership Forum",
            Date = new DateTime(2026, 5, 10),
            Location = "Chicago, IL"
        },
        new EventModel
        {
            Id = 3,
            Name = "Product Launch Night",
            Date = new DateTime(2026, 6, 5),
            Location = "San Francisco, CA"
        }
    ];

    public static EventModel? GetById(int id)
    {
        return Events.FirstOrDefault(evt => evt.Id == id);
    }

    public static EventModel Create(EventModel model)
    {
        var created = new EventModel
        {
            Id = GetNextId(),
            Name = model.Name,
            Date = model.Date,
            Location = model.Location
        };

        Events.Add(created);
        return created;
    }

    public static bool Update(EventModel model)
    {
        var existing = GetById(model.Id);
        if (existing is null)
        {
            return false;
        }

        existing.Name = model.Name;
        existing.Date = model.Date;
        existing.Location = model.Location;
        return true;
    }

    public static bool Delete(int id)
    {
        var existing = GetById(id);
        if (existing is null)
        {
            return false;
        }

        return Events.Remove(existing);
    }

    private static int GetNextId()
    {
        return Events.Count == 0 ? 1 : Events.Max(evt => evt.Id) + 1;
    }
}
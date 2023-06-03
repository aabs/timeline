using System.CommandLine;
using timeline;

public static class Program
{
    public static async Task<int> Main(string[] args)
    {
        var rootCommand = new RootCommand("Sample app for Tracking Events");

        var titleOption = new Option<string>(name: "--title", description: "A short description of the event");
        var dateOption = new Option<DateOnly>(name: "--date", description: "The day the event happened");
        var timeOption = new Option<TimeOnly>(name: "--time", description: "The time the event happened", getDefaultValue: () => new TimeOnly(12, 0));
        var idOption = new Option<int>(name: "--id", description: "The ID of the event");

        var addCommand = new Command("add", "Add an event");
        addCommand.AddOption(titleOption);
        addCommand.AddOption(dateOption);
        addCommand.AddOption(timeOption);
        addCommand.SetHandler(AddEvent, titleOption, dateOption, timeOption);
        rootCommand.Add(addCommand);

        var delCommand = new Command("del", "Remove an event");
        delCommand.AddOption(idOption);
        delCommand.SetHandler(RemoveEvent, idOption);
        rootCommand.Add(delCommand);

        var listCommand = new Command("list", "List all events");
        rootCommand.Add(listCommand);
        listCommand.SetHandler(ListEvents);

        return await rootCommand.InvokeAsync(args);
    }

    private static void AddEvent(string title, DateOnly dateOfEvent, TimeOnly timeOfEvent)
    {
        if (string.IsNullOrWhiteSpace(title))
        {
            Console.Error.WriteLine("Error: Cannot have empty title");
            return;
        }
        using var db = new TimeLineContext();
        var newEvent = new Event(title, dateOfEvent, timeOfEvent);
        db.Add(newEvent);
        db.SaveChanges();
        Console.WriteLine("Added event");
        ListEvents();
    }

    private static void ListEvents()
    {
        using var db = new TimeLineContext();
        foreach (var @event in db.AllEvents().OrderByDescending(x => x.DateOfEvent))
        {
            Console.WriteLine($"{@event.EventId}){@event.DateOfEvent:R}: {@event.Title}");
        }
    }

    private static void RemoveEvent(int eventId)
    {
        using var db = new TimeLineContext();
        var @event = db.Events.Find(eventId);
        if (@event is not null)
        {
            db.Remove(@event);
            db.SaveChanges();
            Console.WriteLine("Event Removed");
        }
        ListEvents();
    }
}
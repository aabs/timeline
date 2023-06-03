// See https://aka.ms/new-console-template for more information

using System.CommandLine;
using System.ComponentModel;

public static class Program
{
    public static async Task<int> Main(string[] args)
    {
        var rootCommand = new RootCommand("Sample app for System.CommandLine");

        var titleOption = new Option<string> (name: "--title", description: "A short description of the event");
        var dateOption = new Option<DateOnly> (name: "--date", description: "The day the event happened");
        var timeOption = new Option<TimeOnly> (name: "--time", description: "The time the event happened");

        var addCommand = new Command("add", "Add an event");
        addCommand.AddOption(titleOption);
        addCommand.AddOption(dateOption);
        addCommand.AddOption(timeOption);
        addCommand.SetHandler(AddEvent, titleOption, dateOption, timeOption);
        rootCommand.Add(addCommand);
        
        var listCommand = new Command("list", "List all events");
        rootCommand.Add(listCommand);
        listCommand.SetHandler(ListEvents );

        return await rootCommand.InvokeAsync(args);

    }

    private static void AddEvent(string title, DateOnly dateOfEvent, TimeOnly timeOfEvent)
    {
        Console.WriteLine($"Adding an event: {dateOfEvent:d} {title}");
    }

    private static void ListEvents()
    {
        Console.WriteLine("hello");
        Console.WriteLine("world");
    }
}


using System.CommandLine;

public class Program
{
    private readonly string[] _args;
    public RootCommand RootCommand { get; set; }
    public EventManager EventMgr { get; set; }
    public Program(string[] args)
    {
        _args = args;
        EventMgr = new();
        RootCommand = SetupCommands();
    }

    private RootCommand SetupCommands()
    {
        var rootCommand = new RootCommand("Sample app for Tracking Events");
        
        var csvOption = new Option<string>(name: "--csv", description: "The CSV export file to import from");
        var fileOption = new Option<string>(name: "--db", description: "The SQLite DB file where the events are stored", getDefaultValue: () => GetDefaultDbPath());
        var titleOption = new Option<string>(name: "--title", description: "A short description of the event");
        var dateOption = new Option<DateOnly>(name: "--date", description: "The day the event happened");
        var timeOption = new Option<TimeOnly>(name: "--time", description: "The time the event happened", getDefaultValue: () => new TimeOnly(12, 0));
        var idOption = new Option<int>(name: "--id", description: "The ID of the event");

        var addCommand = new Command("add", "Add an event");
        addCommand.AddOption(fileOption);
        addCommand.AddOption(titleOption);
        addCommand.AddOption(dateOption);
        addCommand.AddOption(timeOption);
        addCommand.SetHandler(EventMgr.AddEvent, fileOption, titleOption, dateOption, timeOption);
        rootCommand.Add(addCommand);

        var delCommand = new Command("del", "Remove an event");
        delCommand.AddOption(idOption);
        delCommand.SetHandler(EventMgr.RemoveEvent, fileOption, idOption);
        rootCommand.Add(delCommand);

        var listCommand = new Command("list", "List all events");
        rootCommand.Add(listCommand);
        listCommand.SetHandler(EventMgr.ListEvents, fileOption);
        
        var exportCommand = new Command("export", "Export all events to CSV format");
        rootCommand.Add(exportCommand);
        exportCommand.SetHandler(EventMgr.ExportCsv, fileOption);
        
        var importCommand = new Command("import", "Export all events to CSV format");
        importCommand.AddOption(fileOption);
        importCommand.AddOption(csvOption);
        rootCommand.Add(importCommand);
        importCommand.SetHandler(EventMgr.ImportCsv, fileOption, csvOption);
        
        
        
        
        return rootCommand;
    }

    static string GetDefaultDbPath()
    {
        return "timeline.db";
    } 
    public static async Task<int> Main(string[] args)
    {
        var p = new Program(args);
        return await p.RunAsync();
    }

    private async Task<int> RunAsync()
    {
        return await RootCommand.InvokeAsync(_args);
    }
}
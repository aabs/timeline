using System.Text;
using Microsoft.EntityFrameworkCore;
using timeline;

public class EventManager : BaseEventManager
{
    public void AddEvent(string dbPath, string title, DateOnly dateOfEvent, TimeOnly timeOfEvent)
    {
        if (string.IsNullOrWhiteSpace(title))
        {
            Console.Error.WriteLine("Error: Cannot have empty title");
            return;
        }
        using var db = GetDb(dbPath);
        var newEvent = new Event(title, dateOfEvent, timeOfEvent);
        db.Add(newEvent);
        db.SaveChanges();
        Console.WriteLine("Added event");
        ListEvents(dbPath);
    }

    public void ListEvents(string dbPath)
    {
        using var db = GetDb(dbPath);
        foreach (var @event in db.AllEvents().OrderByDescending(x => x.DateOfEvent))
        {
            Console.WriteLine($"{@event.EventId}){@event.DateOfEvent:R}: {@event.Title}");
        }
    }

    public void RemoveEvent(string dbPath, int eventId)
    {
        using var db = GetDb(dbPath);
        var @event = db.Events.Find(eventId);
        if (@event is not null)
        {
            db.Remove(@event);
            db.SaveChanges();
            Console.WriteLine("Event Removed");
        }
        ListEvents(dbPath);
    }

    public void ExportCsv(string dbPath)
    {
        using var db = GetDb(dbPath);
        var sb = new StringBuilder();
        sb.AppendLine($"id,title,date,time");
        foreach (var e in db.AllEvents())
        {
            sb.AppendFormat($"{e.EventId},{e.Title},{e.DateOfEvent},{e.TimeOfEvent}\n");
        }

        Console.WriteLine(sb.ToString());
    }

    public void ImportCsv(string dbPath, string csvPath)
    {
        /*if (File.Exists(dbPath))
        {
            Console.WriteLine("Connot import into existing DB file. Please choose another, or backup and delete this one.");
            return;
        } */
        
        using var db = GetDb(dbPath);
        if (db.Database.EnsureDeleted())
        {
            db.Database.EnsureCreated();
        }
        var lines = File.ReadLines(csvPath);
        if (lines.First() != "id,title,date,time")
        {
            Console.WriteLine("CSV format doesn't match timeline required format. Please use 'id,title,date,time'");
            return;
        }


        foreach (var l in lines.Skip(1))
        {
            var cols = l.Split(',');
            if (cols.Length != 4)
            {
                continue;
            }
            var title = cols[1];
            var d = DateOnly.Parse(cols[2]);
            var t = TimeOnly.Parse(cols[3]);
            db.Events.Add(new Event(title, d, t));
        }
            
        db.SaveChanges();
    }
}
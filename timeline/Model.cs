using System.Data.Common;

namespace timeline;

using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;

public class TimeLineContext : DbContext
{
    public DbSet<Event> Events { get; set; }
    public DbSet<Location> Locations { get; set; }

    public string DbPath { get; set; }

    public TimeLineContext()
    {
        // var folder = Environment.SpecialFolder.LocalApplicationData;
        // var path = Environment.GetFolderPath(folder);
        // DbPath = System.IO.Path.Join(path, "timeline.db");
        DbPath = "timeline.db";
    }

    public IQueryable<Event> AllEvents()
    {
        return from e in Events
            select e;
    }
    // The following configures EF to create a Sqlite database file in the
    // special "local" folder for your platform.
    protected override void OnConfiguring(DbContextOptionsBuilder options)
        => options.UseSqlite($"Data Source={DbPath}");
}

public class Event
{
    public Event(string title, DateOnly dateOfEvent, TimeOnly timeOfEvent)
    {
        if (string.IsNullOrWhiteSpace(title))
        {
            throw new ArgumentException("Invalid title");
        }
        Title = title;
        DateOfEvent = dateOfEvent;
        TimeOfEvent = timeOfEvent;
        Created = DateTimeOffset.Now;
    }

    public int EventId { get; set; }
    public string Title { get; set; }
    public DateOnly DateOfEvent { get; set; }
    public TimeOnly TimeOfEvent { get; set; }
    public Location? LocationOfEvent { get; set; }
    public DateTimeOffset Created { get; set; }
}

public class Location
{
    public Location(string nameOfLocation)
    {
        NameOfLocation = nameOfLocation;
    }

    public int LocationId { get; set; }
    public string NameOfLocation { get; set; }
    public decimal? Latitude { get; set; }
    public decimal? Longitude { get; set; }
} 
using System.Data.Common;

namespace timeline;

using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;

public class TimeLineContext : DbContext
{
    public DbSet<Event> Events { get; set; }
    public DbSet<Location> Locations { get; set; }

    public string DbPath { get; }

    public TimeLineContext()
    {
        var folder = Environment.SpecialFolder.LocalApplicationData;
        var path = Environment.GetFolderPath(folder);
        DbPath = System.IO.Path.Join(path, "timeline.db");
    }

    // The following configures EF to create a Sqlite database file in the
    // special "local" folder for your platform.
    protected override void OnConfiguring(DbContextOptionsBuilder options)
        => options.UseSqlite($"Data Source={DbPath}");
}

public class Event
{
    public int EventId { get; set; }
    public string Title { get; set; }
    public DateOnly DateOfEvent { get; set; }
    public TimeOnly? TimeOfEvent { get; set; }
    public Location LocationOfEvent { get; set; }
    private DateTimeOffset Created { get; set; }
}

public class Location
{
    public int LocationId { get; set; }
    public string NameOfLocation { get; set; }
    public decimal Latitude { get; set; }
    public decimal Longitude { get; set; }
} 
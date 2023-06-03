﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using timeline;

#nullable disable

namespace timeline.Migrations
{
    [DbContext(typeof(TimeLineContext))]
    partial class TimeLineContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder.HasAnnotation("ProductVersion", "7.0.5");

            modelBuilder.Entity("timeline.Event", b =>
                {
                    b.Property<int>("EventId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<DateTimeOffset>("Created")
                        .HasColumnType("TEXT");

                    b.Property<DateOnly>("DateOfEvent")
                        .HasColumnType("TEXT");

                    b.Property<int?>("LocationOfEventLocationId")
                        .HasColumnType("INTEGER");

                    b.Property<TimeOnly?>("TimeOfEvent")
                        .HasColumnType("TEXT");

                    b.Property<string>("Title")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.HasKey("EventId");

                    b.HasIndex("LocationOfEventLocationId");

                    b.ToTable("Events");
                });

            modelBuilder.Entity("timeline.Location", b =>
                {
                    b.Property<int>("LocationId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<decimal?>("Latitude")
                        .HasColumnType("TEXT");

                    b.Property<decimal?>("Longitude")
                        .HasColumnType("TEXT");

                    b.Property<string>("NameOfLocation")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.HasKey("LocationId");

                    b.ToTable("Locations");
                });

            modelBuilder.Entity("timeline.Event", b =>
                {
                    b.HasOne("timeline.Location", "LocationOfEvent")
                        .WithMany()
                        .HasForeignKey("LocationOfEventLocationId");

                    b.Navigation("LocationOfEvent");
                });
#pragma warning restore 612, 618
        }
    }
}

using Microsoft.EntityFrameworkCore;
using Sportradar.Core.Entities;
using Sportradar.Core.Value_Objects;
using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace Sportradar.Infrastructure;

public static class DbSeed
{
    public static async Task SeedAsync(ApplicationDbContext context)
    {
        if (AppDomain.CurrentDomain.FriendlyName.Contains("ef"))
            return;

        await SeedEventsAsync(context);
        await SeedParticipantsAsync(context);
        await SeedResultsAsync(context);



    }
    public static async Task SeedResultsAsync(ApplicationDbContext context)
    {
        if (context.Results.Any()) return;

        var options = new JsonSerializerOptions
        {
            PropertyNameCaseInsensitive = true
        };
        options.Converters.Add(new JsonStringEnumConverter());

        var oooPath = Path.Combine(AppContext.BaseDirectory, "seed", "results", "oneOnOneResults.json");
        var oooJson = await File.ReadAllTextAsync(oooPath);
        var oneOnOneResults = JsonSerializer.Deserialize<List<OneOnOneResult>>(oooJson, options)!;

        var teamPath = Path.Combine(AppContext.BaseDirectory, "seed", "results", "teamResults.json");
        var teamJson = await File.ReadAllTextAsync(teamPath);
        var teamResults = JsonSerializer.Deserialize<List<TeamResult>>(teamJson, options)!;

        var ffaPath = Path.Combine(AppContext.BaseDirectory, "seed", "results", "freeForAllResults.json");
        var ffaJson = await File.ReadAllTextAsync(ffaPath);
        var ffaResults = JsonSerializer.Deserialize<List<FreeForAllResult>>(ffaJson, options)!;

        var allResults = new List<Result>();
        allResults.AddRange(oneOnOneResults);
        allResults.AddRange(teamResults);
        allResults.AddRange(ffaResults);

        var eventIds = context.Events.Select(e => e.Id).ToHashSet();

        foreach (var r in allResults)
        {
            if (!eventIds.Contains(r.EventId))
            {
                throw new Exception($"Missing Event for Result {r.Id}, EventId: {r.EventId}");
            }
        }
        context.Results.AddRange(allResults);

        await context.SaveChangesAsync();
    }

    public static async Task SeedEventsAsync(ApplicationDbContext context)
    {
        if (context.Events.Any()) return;

        var options = new JsonSerializerOptions
        {
            PropertyNameCaseInsensitive = true
        };
        options.Converters.Add(new JsonStringEnumConverter());

        var oooPath = Path.Combine(AppContext.BaseDirectory, "seed", "events", "oneOnOneEvents.json");
        var oooJson = await File.ReadAllTextAsync(oooPath);
        var oneOnOne = JsonSerializer.Deserialize<List<OneOnOneEvent>>(oooJson, options)!;

        var teamPath = Path.Combine(AppContext.BaseDirectory, "seed", "events", "teamEvents.json");
        var teamJson = await File.ReadAllTextAsync(teamPath);
        var team = JsonSerializer.Deserialize<List<TeamEvent>>(teamJson, options)!;

        var ffaPath = Path.Combine(AppContext.BaseDirectory, "seed", "events", "freeForAllEvents.json");
        var ffaJson = await File.ReadAllTextAsync(ffaPath);
        var ffaSeeds = JsonSerializer.Deserialize<List<FreeForAllEventSeed>>(ffaJson, options)!;

        var ffaEvents = ffaSeeds.Select(e => new FreeForAllEvent
        {
            Id = e.Id,
            StartTime = e.StartTime,
            EndTime = e.EndTime,
            Title = e.Title,
            SportId = e.SportId,
            LocationId = e.LocationId,
            CreatedAt = e.CreatedAt,
            Status = e.Status
        }).ToList();

        context.Events.AddRange(oneOnOne);
        context.Events.AddRange(team);
        context.Events.AddRange(ffaEvents);

        await context.SaveChangesAsync();
    }

    public static async Task SeedParticipantsAsync(ApplicationDbContext context)
    {
        var options = new JsonSerializerOptions
        {
            PropertyNameCaseInsensitive = true
        };
        options.Converters.Add(new JsonStringEnumConverter());
        var path = Path.Combine(AppContext.BaseDirectory, "seed", "events", "freeForAllEvents.json");
        var json = await File.ReadAllTextAsync(path);
        var seeds = JsonSerializer.Deserialize<List<FreeForAllEventSeed>>(json, options)!;

        foreach (var seed in seeds)
        {
            var ev = await context.Events
                .OfType<FreeForAllEvent>()
                .Include(e => e.Participants)
                .FirstAsync(e => e.Id == seed.Id);

            foreach (var playerId in seed.Participants)
            {
                var player = await context.Players.FindAsync(playerId);
                if (player != null)
                {
                    ev.Participants.Add(player);
                }
            }
        }

        await context.SaveChangesAsync();
    }
}

public class FreeForAllEventSeed
{
    public Guid Id { get; set; }
    public DateTime StartTime { get; set; }
    public DateTime EndTime { get; set; }
    public string Title { get; set; } = "";
    public DateTime CreatedAt { get; set; }
    public Status Status { get; set; }
    public Guid SportId { get; set; }
    public Guid LocationId { get; set; }
    public List<Guid> Participants { get; set; } = new();
}



using Microsoft.EntityFrameworkCore;
using Sportradar.Backend.ConfigExtentions;
using Sportradar.Infrastructure;
using System;

namespace Sportradar.Backend;

public class Program
{
    public static async Task Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

        // Add services to the container.

        builder.Services.ConfigureBaselineServices(builder.Configuration);

        var app = builder.Build();

        // Configure the HTTP request pipeline.
        if (app.Environment.IsDevelopment())
        {
            app.MapOpenApi();
        }

        using (var scope = app.Services.CreateScope())
        {
            var context = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();
            Console.WriteLine(context.Database.GetConnectionString());
            await DbSeed.SeedAsync(context);
        }

        //app.UseHttpsRedirection();

        //app.UseAuthorization();
        app.UseCors("AllowFrontend");

        app.MapControllers();

        app.Run();
    }
}

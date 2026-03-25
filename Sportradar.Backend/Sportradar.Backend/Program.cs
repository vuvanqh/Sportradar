
using Microsoft.EntityFrameworkCore;
using Serilog;
using Sportradar.Backend.ConfigExtentions;
using Sportradar.Infrastructure;
using System;

namespace Sportradar.Backend;

public class Program
{
    public static async Task Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);


        builder.Services.ConfigureBaselineServices(builder.Configuration);

        builder.Host.UseSerilog((HostBuilderContext context, IServiceProvider services, LoggerConfiguration loggerConfiguration) => {
            loggerConfiguration.ReadFrom.Configuration(context.Configuration) 
                               .ReadFrom.Services(services); 
        });

        var app = builder.Build();

        using (var scope = app.Services.CreateScope())
        {
            var context = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();

            await context.Database.MigrateAsync();

            await DbSeed.SeedAsync(context);
        }

        app.UseRouting();

        app.UseSerilogRequestLogging();
        //app.UseHttpsRedirection();

        //app.UseAuthorization();
        app.UseCors("AllowFrontend");

        app.UseSwagger();
        app.UseSwaggerUI(c =>
        {
            c.SwaggerEndpoint("/swagger/v1/swagger.json", "Sportradar API v1");
        });

        app.MapControllers();

        app.Run();
    }
}

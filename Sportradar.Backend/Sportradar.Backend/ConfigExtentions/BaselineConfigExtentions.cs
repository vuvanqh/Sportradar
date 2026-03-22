using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Sportradar.Infrastructure;
using System.Text.Json.Serialization;

namespace Sportradar.Backend.ConfigExtentions;

public static class BaselineConfigExtentions
{
    public static void ConfigureBaselineServices(this IServiceCollection services, IConfiguration config)
    {
        services.AddRouting(options =>
        {
            options.LowercaseUrls = true;
        });

        services.AddControllers(options =>
        {
            options.Filters.Add(new ConsumesAttribute("application/json"));
            options.Filters.Add(new ProducesAttribute("application/json"));
        }).AddJsonOptions(options =>
        {
            options.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter());
        });

        services.AddDbContext<ApplicationDbContext>(options =>
        {
            options.UseSqlServer(config.GetConnectionString("Default"), sqlOptions =>
            {
                sqlOptions.UseQuerySplittingBehavior(QuerySplittingBehavior.SplitQuery);
                sqlOptions.EnableRetryOnFailure(
                    maxRetryCount: 5,
                    maxRetryDelay: TimeSpan.FromSeconds(5),
                    errorNumbersToAdd: null);
            });
        });
    }
}

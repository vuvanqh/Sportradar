using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi;
using Sportradar.Core.Application.DTOs;
using Sportradar.Core.Application.ServiceContracts;
using Sportradar.Core.Application.Services;
using Sportradar.Core.Domain;
using Sportradar.Core.Domain.RepositoryContracts;
using Sportradar.Infrastructure;
using Sportradar.Infrastructure.Repositories;
using System.Text.Json.Serialization;
using System.Text.Json.Serialization.Metadata;

namespace Sportradar.Backend.ConfigExtentions;

public static class BaselineConfigExtentions
{
    public static void ConfigureBaselineServices(this IServiceCollection services, IConfiguration config)
    {
        services.AddRouting(options =>
        {
            options.LowercaseUrls = true;
        });

        services.AddCors(options =>
        {
            options.AddPolicy("AllowFrontend",
                policy =>
                {
                    policy
                        .WithOrigins(config.GetSection("AllowedOrigins").Get<string[]>()!)
                        .AllowAnyHeader()
                        .AllowAnyMethod()
                        .AllowCredentials(); 
                });
        });
        services.AddControllers(options =>
        {
            options.Filters.Add(new ConsumesAttribute("application/json"));
            options.Filters.Add(new ProducesAttribute("application/json"));
        });

        services.AddDbContext<ApplicationDbContext>(options =>
        {
            options.UseSqlServer(config.GetConnectionString("Default"), sqlOptions =>
            {
                sqlOptions.UseQuerySplittingBehavior(QuerySplittingBehavior.SplitQuery);
                options.EnableSensitiveDataLogging();
                sqlOptions.EnableRetryOnFailure(
                    maxRetryCount: 5,
                    maxRetryDelay: TimeSpan.FromSeconds(5),
                    errorNumbersToAdd: null);
            });

        });

        services.Configure<JsonOptions>(options =>
        {
            options.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter());
            options.JsonSerializerOptions.TypeInfoResolver  = new DefaultJsonTypeInfoResolver
            {
                Modifiers =
                {
                    ti =>
                    {
                        if (ti.Type == typeof(EventResponse))
                        {
                            ti.PolymorphismOptions = new JsonPolymorphismOptions
                            {
                                TypeDiscriminatorPropertyName = "eventType",
                                DerivedTypes =
                                {
                                    new JsonDerivedType(typeof(TeamEventResponse), "Team"),
                                    new JsonDerivedType(typeof(OneOnOneEventResponse), "OneOnOne"),
                                    new JsonDerivedType(typeof(FreeForAllEventResponse), "FreeForAll")
                                }
                            };
                        }

                        if (ti.Type == typeof(ResultDTO))
                        {
                            ti.PolymorphismOptions = new JsonPolymorphismOptions
                            {
                                TypeDiscriminatorPropertyName = "resultType",
                                DerivedTypes =
                                {
                                    new JsonDerivedType(typeof(TeamResultDTO), "Team"),
                                    new JsonDerivedType(typeof(OneOnOneResultDTO), "OneOnOne"),
                                    new JsonDerivedType(typeof(FreeForAllResultDTO), "FreeForAll")
                                }
                            };
                        }
                    }
                }
            };
        });

        //services
        services.AddScoped<IEventService, EventService>();
        services.AddScoped<ITeamService, TeamService>();
        services.AddScoped<IPlayerService, PlayerService>();
        services.AddScoped<ILocationService, LocationService>();
        services.AddScoped<ICompetitionService, CompetitionService>();
        services.AddScoped<ISportService, SportService>();

        //repositories
        services.AddScoped<IEventRepository, EventRepository>();
        services.AddScoped<IPlayerRepository, PlayerRepository>();
        services.AddScoped<ILocationRepository, LocationRepository>();
        services.AddScoped<ISportRepository, SportRepository>();
        services.AddScoped<ICompetitionRepository, CompetitionRepository>();
        services.AddScoped<ITeamRepository, TeamRepository>();

        services.AddSwaggerGen(options =>
        {
            options.IncludeXmlComments(Path.Combine(AppContext.BaseDirectory, "Sportradar.Backend.xml"));
            options.SwaggerDoc("v1", new OpenApiInfo
            {
                Title = "Sportradar API",
                Version = "v1",
                Description = "API for managing sports events, players, and locations."
            });
            options.UseAllOfForInheritance();
            options.UseOneOfForPolymorphism();
        });
    }
}

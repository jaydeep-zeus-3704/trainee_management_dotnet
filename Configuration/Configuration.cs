using System.Text;
using System.Text.Json.Serialization;
using HealthChecks.UI.Client;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Diagnostics.HealthChecks;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Diagnostics.HealthChecks;
using Microsoft.Extensions.Primitives;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi; // Use Microsoft.OpenApi.Models if using older Swagger versions
using RabbitMQ.Client;
using Serilog;
using trainee_management.Database;
using trainee_management.Services;

namespace trainee_management.Configuration;

public static class Configuration
{
    public static void AddApplicationLogging(this WebApplicationBuilder builder)
    {
        Log.Logger = new LoggerConfiguration()
            .MinimumLevel.Debug()
            .WriteTo.Console()
            .CreateLogger();

        builder.Services.AddSerilog();
    }

    public static IServiceCollection  ConfigureHealthCheck(this IServiceCollection services,IConfiguration configuration)
    {
        string redisConn = configuration.GetConnectionString("RedisConnection")!;

         ConnectionFactory rabbitMqFactory = new ConnectionFactory
        {
            HostName = configuration["RabbitMQ:Host"]!,
            UserName = Environment.GetEnvironmentVariable("RabbitMQUsername")!,
            Password = Environment.GetEnvironmentVariable("RabbitMQPassword")!,
        };
        services.AddHealthChecks()
        .AddMySql(
            connectionString:Environment.GetEnvironmentVariable("ConnectionString")!,
             name:"mysql_database",
             failureStatus:HealthStatus.Unhealthy,
             tags:["ready"]
        )
        .AddRedis(
            redisConnectionString:redisConn,
            name:"redis_cache",
            failureStatus:HealthStatus.Degraded,
            tags:["ready"]
        )
        .AddRabbitMQ(
                name: "rabbitmq_broker",
                failureStatus: HealthStatus.Unhealthy,
                tags: ["ready"]);
        ;
        return services;
    }






    public static async Task<IServiceCollection> AddMessagingServicesAsync(this IServiceCollection services, IConfiguration configuration)
    {
        ConnectionFactory factory = new ConnectionFactory
        {
            HostName = configuration["RabbitMQ:Host"]!,
            UserName = Environment.GetEnvironmentVariable("RabbitMQUsername")!,
            Password = Environment.GetEnvironmentVariable("RabbitMQPassword")!,
        };

        IConnection connection = await factory.CreateConnectionAsync();

        services.AddSingleton(connection);
        services.AddSingleton<IRabbitMQPublisher, RabbitMQPublisher>();

        return services;
    }

    public static IServiceCollection AddBusinessServices(this IServiceCollection services)
    {
        services.AddScoped<ItraineeService, TraineeService>();
        services.AddScoped<IUserService, UserService>();
        services.AddScoped<IMentorService, MentorService>();
        services.AddScoped<ILearningTaskService, LearningTaskService>();
        services.AddScoped<ITaskAssignmentService, TaskAssignmentService>();
        services.AddScoped<ISubmissionService, SubmissionService>();
        services.AddScoped<IReviewService, ReviewService>();
        services.AddScoped<IFileStorageSerivce, LocalStorageService>();
        services.AddScoped<ICacheService, CacheService>();

        return services;
    }

    public static IServiceCollection AddDataStores(this IServiceCollection services, IConfiguration configuration)
    {
        // Redis
        string redisConn = configuration.GetConnectionString("RedisConnection")!;
        services.AddStackExchangeRedisCache(options =>
        {
            options.Configuration = redisConn;
        });

        // MySQL Database
        string dbConn = Environment.GetEnvironmentVariable("ConnectionString")!;
        services.AddDbContext<AppDBContext>(options =>
            options.UseMySql(dbConn, ServerVersion.AutoDetect(dbConn)));

        return services;
    }

    public static IServiceCollection AddSecurityAndCors(this IServiceCollection services, IConfiguration configuration, string corsPolicyName)
    {
        services.AddCors(options =>
        {
            options.AddPolicy(name: corsPolicyName, policy =>
            {
                policy.WithOrigins("https://localhost:5173", "http://localhost:3000")
                      .AllowAnyHeader()
                      .AllowAnyMethod();
            });
        });

        services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
            .AddJwtBearer(options =>
            {
                options.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuer = true,
                    ValidateAudience = true,
                    ValidateLifetime = true,
                    ValidateIssuerSigningKey = true,
                    ValidIssuer = configuration["Jwt:Issuer"],
                    ValidAudience = configuration["Jwt:Audience"],
                    IssuerSigningKey = new SymmetricSecurityKey(
                        Encoding.UTF8.GetBytes(Environment.GetEnvironmentVariable("Key")!)
                    ),
                };
            });

        services.AddAuthorization();

        return services;
    }

    public static IServiceCollection AddApiDocumentation(this IServiceCollection services)
    {
        services.AddControllers().AddJsonOptions(options =>
        {
            options.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter());
        });

        services.AddSwaggerGen(options =>
        {
            options.SwaggerDoc("v1", new OpenApiInfo { Title = "MyAPI", Version = "v1" });
            options.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
            {
                In = ParameterLocation.Header,
                Description = "Please enter token",
                Name = "Authorization",
                Type = SecuritySchemeType.Http,
                BearerFormat = "JWT",
                Scheme = "Bearer"
            });

            options.AddSecurityRequirement(document => new OpenApiSecurityRequirement
            {
                [new OpenApiSecuritySchemeReference("Bearer", document)] = []
            });
        });

        return services;
    }
}

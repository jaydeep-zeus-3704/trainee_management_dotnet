using DotNetEnv;
using HealthChecks.UI.Client;
using Microsoft.AspNetCore.Diagnostics.HealthChecks;
using trainee_management.Configuration;

using trainee_management.Middlewares;

Env.Load();

WebApplicationBuilder builder = WebApplication.CreateBuilder(args);



// Configure Server Limits
builder.WebHost.ConfigureKestrel(serverOptions =>
{
    serverOptions.Limits.MaxRequestBodySize = 524288000; 
});

// Configure Functional Areas via Extensions
builder.AddApplicationLogging();
const string myAllowSpecificOrigins = "_myAllowSpecificOrigins";

builder.Services.AddSecurityAndCors(builder.Configuration, myAllowSpecificOrigins);
builder.Services.AddDataStores(builder.Configuration);
builder.Services.AddBusinessServices();
builder.Services.AddApiDocumentation();
builder.Services.ConfigureHealthCheck(builder.Configuration);

// Handle Async Extensions
await builder.Services.AddMessagingServicesAsync(builder.Configuration);

WebApplication app = builder.Build();

app.MapHealthChecks("/health/live", new HealthCheckOptions
{
    Predicate = check => check.Tags.Contains("live"),
    ResponseWriter = UIResponseWriter.WriteHealthCheckUIResponse
});


app.MapHealthChecks("/health/ready", new HealthCheckOptions
{
    Predicate = check => check.Tags.Contains("ready"),
    ResponseWriter = UIResponseWriter.WriteHealthCheckUIResponse
});



    app.UseSwagger();
    app.UseSwaggerUI();


app.UseMiddleware<GlobalExceptionHandlingMiddleware>();
app.UseAuthentication();
app.UseHttpsRedirection();
app.UseCors(myAllowSpecificOrigins);
app.UseAuthorization();
app.MapControllers();

app.Run();

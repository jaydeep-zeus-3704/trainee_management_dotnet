using Microsoft.EntityFrameworkCore;
using trainee_management.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();

builder.Services.AddScoped<ItraineeService,TraineeService>();

string? connectionString=builder.Configuration.GetConnectionString("Database");

if (!string.IsNullOrEmpty(connectionString))
{
 builder.Services.AddDbContext<AppDBContext>(options =>options.UseInMemoryDatabase(connectionString));
}
else
{
    builder.Services.AddDbContext<AppDBContext>(options =>options.UseInMemoryDatabase("Trainee"));
}

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
     app.UseSwaggerUi(options =>
    {
        options.DocumentPath = "/openapi/v1.json";
    });
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();

using System.Text.Json.Serialization;
using Microsoft.EntityFrameworkCore;
using trainee_management.Middlewares;
using trainee_management.Services;
using trainee_management.Database;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using Microsoft.OpenApi.Models;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.


// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi

builder.Services.AddScoped<ItraineeService, TraineeService>();
builder.Services.AddScoped<IUserService, UserService>();
builder.Services.AddScoped<IMentorService,MentorService>();
builder.Services.AddScoped<ILearningTaskService,LearningTaskService>();
builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme).AddJwtBearer(options =>
{
    options.TokenValidationParameters=new TokenValidationParameters
    {
        ValidateIssuer = true,
        ValidateAudience = true,
        ValidateLifetime = true,
        ValidateIssuerSigningKey = true,
        ValidIssuer = builder.Configuration["Jwt:Issuer"],
        ValidAudience = builder.Configuration["Jwt:Audience"],
        IssuerSigningKey = new SymmetricSecurityKey(
            Encoding.UTF8.GetBytes(
                builder.Configuration["Jwt:Key"]!
            )
        ),

    };
});

var myAllowSpecificOrigins = "_myAllowSpecificOrigins";

builder.Services.AddCors(options =>
{
    options.AddPolicy(name: myAllowSpecificOrigins,
        policy =>
        {
            policy.WithOrigins("https://localhost:5173", "http://localhost:3000") // Your frontend URLs
                  .AllowAnyHeader()
                  .AllowAnyMethod();
        });
});





builder.Services.AddAuthorization();
string? connectionString = builder.Configuration.GetConnectionString("Database");


builder.Services.AddControllers().AddJsonOptions(options =>
    {
        options.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter());
    });

builder.Services.AddDbContext<AppDBContext>(options => options.UseMySql(connectionString, ServerVersion.AutoDetect(connectionString)));
builder.Services.AddSwaggerGen(options=>{
    options.SwaggerDoc("v1",new OpenApiInfo
    {
        Title="trainee_management_api",
        Version="v1"
    });

    options.AddSecurityDefinition("Bearer",new OpenApiSecurityScheme
    {
       Name="Authorization" ,
       Type=SecuritySchemeType.Http,
       Scheme="Bearer",
       BearerFormat="JWT",
       In=ParameterLocation.Header,
       Description="Enter your jwt token here"
    });
    
     options.AddSecurityRequirement(new OpenApiSecurityRequirement
     {
         {
             new OpenApiSecurityScheme
             {
                 Reference=new OpenApiReference
                 {
                     Type=ReferenceType.SecurityScheme,
                     Id="Bearer"
                 }
             },
             Array.Empty<string>()
         }
     });
  
   });

var app = builder.Build();





// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseMiddleware<GlobalExceptionHandlingMiddleware>();

app.UseAuthentication();

app.UseHttpsRedirection();


app.UseCors(myAllowSpecificOrigins);

app.UseAuthorization();



app.MapControllers();

app.Run();

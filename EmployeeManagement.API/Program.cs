using EmployeeManagement.Persistence;
using EmployeeManagement.Application;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;
using Serilog;

var builder = WebApplication.CreateBuilder(args);

// Configure Serilog
Log.Logger = new LoggerConfiguration()
    .MinimumLevel.Error()
    .WriteTo.Console()
    .WriteTo.File("Logs/log-.txt", rollingInterval: RollingInterval.Day)
    .CreateLogger();

builder.Host.UseSerilog();

// Add services to the container.
builder.Services.AddControllers();

// Add application services
builder.Services.InjectApplicationDependencies(builder.Configuration);

// Enable Swagger only in development
if (builder.Environment.IsDevelopment())
{
    builder.Services.AddEndpointsApiExplorer();
    builder.Services.AddSwaggerGen(options =>
    {
        options.SwaggerDoc("v1", new OpenApiInfo
        {
            Title = "Employee Management API",
            Version = "v1",
            Description = "API for managing employees with CRUD operations."
        });
    });
}

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseHsts(); // Enable HSTS for security
    app.UseHttpsRedirection(); // Only enforce HTTPS in non-development environments
}

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(options =>
    {
        options.SwaggerEndpoint("/swagger/v1/swagger.json", "Employee Management API V1");
    });
}
app.UseSerilogRequestLogging(); // Middleware to log requests
app.UseAuthorization();
app.MapControllers();
app.Run();

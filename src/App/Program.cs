using System.Reflection;
using App.Extensions;
using App.Infrastructure;
using Application;
using Infrastructure;
using Serilog;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddSwaggerWithConfiguration();

builder.Services.AddApplication();
builder.Services.AddInfrastructure(builder.Configuration);

builder.Services.AddExceptionHandler<GlobalExceptionHandler>();

builder.Services.AddProblemDetails();

builder.Services.AddEndpoints(Assembly.GetExecutingAssembly());

builder.Host.UseSerilogWithConfiguration();

var app = builder.Build();

app.MapEndpoints();

if (app.Environment.IsDevelopment())
{
    app.UseSwaggerWithUI();

    app.ApplyMigrations();
}

app.UseHttpsRedirection();

app.UseSerilogRequestLogging();

app.UseRequestContextLogging();

app.UseExceptionHandler();

app.Run();



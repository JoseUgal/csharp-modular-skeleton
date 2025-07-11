using System.Reflection;
using App.Extensions;
using App.Infrastructure;
using Infrastructure.Extensions;
using Serilog;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddSwaggerWithConfiguration();

builder.Services.InstallServicesFromAssemblies(
    builder.Configuration,
    Infrastructure.AssemblyReference.Assembly
);

builder.Services.InstallModulesFromAssemblies(
    builder.Configuration,
    Modules.Users.Infrastructure.AssemblyReference.Assembly
);

builder.Services.AddExceptionHandler<GlobalExceptionHandler>();

builder.Services.AddProblemDetails();

builder.Services.AddEndpoints(Assembly.GetExecutingAssembly());

builder.Host.UseSerilogWithConfiguration();

var app = builder.Build();

app.MapEndpoints();

if (app.Environment.IsDevelopment())
{
    app.UseSwaggerWithUI();
}

app.UseHttpsRedirection();

app.UseSerilogRequestLogging();

app.UseRequestContextLogging();

app.UseExceptionHandler();

app.Run();



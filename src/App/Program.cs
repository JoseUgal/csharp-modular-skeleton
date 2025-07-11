using System.Reflection;
using App.Extensions;
using App.Infrastructure;
using Infrastructure.Extensions;
using Serilog;

var builder = WebApplication.CreateBuilder(args);

builder.Services.InstallServicesFromAssemblies(
    builder.Configuration,
    App.AssemblyReference.Assembly,
    Infrastructure.AssemblyReference.Assembly
);

builder.Services.InstallModulesFromAssemblies(
    builder.Configuration,
    Modules.Users.Infrastructure.AssemblyReference.Assembly
);

builder.Services.AddEndpoints(Assembly.GetExecutingAssembly());

builder.Host.UseSerilogWithConfiguration();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwaggerWithUi();
}

app.UseHttpsRedirection();

app.UseSerilogRequestLogging();

app.UseRequestContextLogging();

app.UseExceptionHandler();

app.MapEndpoints();

app.Run();



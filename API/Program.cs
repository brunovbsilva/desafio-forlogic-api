using System.Text.Json.Serialization;
using API.Configurations;
using API.Middlewares;
using HealthChecks.UI.Client;
using Infra.IoC;
using Infra.Security;
using Infra.Utils.Configuration;
using Microsoft.AspNetCore.Diagnostics.HealthChecks;
using Serilog;

var builder = WebApplication.CreateBuilder(args);

var apiName = "ForLogic API";

builder.Services.AddControllers().AddJsonOptions(x => x.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles);
builder.Services.Configure<AppSettings>(builder.Configuration.GetSection("AppSettings"));
builder.Services.AddOpenApi();

#region Local Injections
builder.Services.AddLocalServices(builder.Configuration);
builder.Services.AddLocalHttpClients(builder.Configuration);
builder.Services.AddLocalUnitOfWork(builder.Configuration);
builder.Services.AddLocalCache(builder.Configuration);
builder.Services.AddLocalHealthChecks(builder.Configuration);
#endregion

#region Security Injections
builder.Services.AddLocalCors();
#endregion

builder.Host.UseSerilog((hostingContext, loggerConfiguration) => loggerConfiguration.ReadFrom.Configuration(hostingContext.Configuration));

builder.Services.AddOptions();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
    app.UseCors("AllowAll");
    app.ApplyMigration();
}
else
{
    app.UseCors("AllowSpecificOrigin");
}

app.MapHealthChecks("health", new HealthCheckOptions { ResponseWriter = UIResponseWriter.WriteHealthCheckUIResponse });
app.MapControllers();
app.UseHttpsRedirection();
app.UseAuthorization();

#region Middlewares
app.UseMiddleware<ControllerMiddleware>();
app.UseMiddleware<RedisCacheMiddleware>();
#endregion

try
{
    Log.Information($"[{apiName}] Starting the application...");
    app.Run();
}
catch (Exception ex)
{
    Log.Fatal(ex, $"[{apiName}] Application failed to start");
}
finally
{
    Log.Information($"[{apiName}] Finishing the application...");
    Log.CloseAndFlush();
}


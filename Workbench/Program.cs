using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Npgsql;
using OpenTelemetry;
using OpenTelemetry.Metrics;
using OpenTelemetry.Resources;
using OpenTelemetry.Trace;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddOpenTelemetry()
    .ConfigureResource(resource => resource.AddService("Sample"))
    .WithTracing(tracing => tracing
        .AddHttpClientInstrumentation()
        .AddAspNetCoreInstrumentation()
        .AddNpgsql())
    .WithMetrics(metrics => metrics
        .AddHttpClientInstrumentation()
        .AddAspNetCoreInstrumentation());

builder.Logging.AddOpenTelemetry(options =>
{
    options.IncludeScopes = true;
    options.IncludeFormattedMessage = true;
});

builder.Services.AddOpenTelemetry().UseOtlpExporter();

var app = builder.Build();

// the magic 🪄
var global = app
    .MapGroup(string.Empty)
    .AddEndpointFilter<ScreamingFilter>()
    .AddEndpointFilterFactory((context, next) =>
    {
        var ii = 1;
        // if ()
        return next;
    });

global.MapGet("/hi", () => "Hi");
global.MapGroup("/what")
    .MapGet("/now", () => "🤷");
global.MapGroup("/what").MapGet("/nowagain", () => "🤷");

app.Run();

public class ScreamingFilter : IEndpointFilter
{
    public async ValueTask<object?> InvokeAsync(
        EndpointFilterInvocationContext context,
        EndpointFilterDelegate next)
    {
        var result = await next(context);
        return result is string s
            ? $"{s}!!!!"
            : result;
    }
}
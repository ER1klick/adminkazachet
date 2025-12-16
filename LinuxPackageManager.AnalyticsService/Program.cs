using LinuxPackageManager.AnalyticsService.Services;
using OpenTelemetry.Metrics;
using OpenTelemetry.Resources;
using OpenTelemetry.Trace;

var builder = WebApplication.CreateBuilder(args);
var app = builder.Build();

var serviceName = builder.Environment.ApplicationName; 

builder.Services.AddOpenTelemetry()
    .ConfigureResource(resource => resource.AddService(serviceName))
    .WithTracing(tracing =>
    {
        tracing
            .AddAspNetCoreInstrumentation()
            .AddHttpClientInstrumentation()
            .AddGrpcClientInstrumentation()
            .AddSource("MassTransit"); 

        tracing.AddZipkinExporter(zipkin =>
        {
            var zipkinUrl = builder.Configuration["Zipkin:Endpoint"] ?? "http://localhost:9411/api/v2/spans";
            zipkin.Endpoint = new Uri(zipkinUrl);
        });
    })
    .WithMetrics(metrics =>
    {
        metrics
            .AddAspNetCoreInstrumentation()
            .AddHttpClientInstrumentation()
            .AddRuntimeInstrumentation();

        metrics.AddPrometheusExporter();
    });
builder.Services.AddGrpc();
app.MapGrpcService<AnalyticsGrpcService>();
app.MapGet("/", () => "Communication with gRPC endpoints must be made through a gRPC client.");
app.UseOpenTelemetryPrometheusScrapingEndpoint();
app.Run();
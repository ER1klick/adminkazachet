using LinuxPackageManager.AuditService;
using MassTransit;
using LinuxPackageManager.Events; 
using OpenTelemetry.Metrics;
using OpenTelemetry.Resources;
using OpenTelemetry.Trace;

var builder = Host.CreateApplicationBuilder(args);

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
builder.Services.AddMassTransit(x =>
{
    x.AddConsumer<PackageCreatedConsumer>();
    x.AddConsumer<PackageDeletedConsumer>();
    x.AddConsumer<UserRatedConsumer>();

    x.UsingRabbitMq((context, cfg) =>
    {
        var rabbitHost = builder.Configuration["RabbitMq:Host"] ?? "localhost";

        cfg.Host(rabbitHost, "/", h => {
            h.Username("guest");
            h.Password("guest");
        });
        
        cfg.ReceiveEndpoint("audit-service-user-rated", e =>
        {
            e.ConfigureConsumer<UserRatedConsumer>(context);

            e.Bind<UserRatedEvent>();
        });

        cfg.ConfigureEndpoints(context);
    });
});

var host = builder.Build();
host.Run();
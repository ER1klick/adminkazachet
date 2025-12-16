using LinuxPackageManager.Events;
using MassTransit;
using Microsoft.Extensions.Logging;

namespace LinuxPackageManager.AuditService;

public class PackageDeletedConsumer : IConsumer<PackageDeletedEvent>
{
    private readonly ILogger<PackageDeletedConsumer> _logger;

    public PackageDeletedConsumer(ILogger<PackageDeletedConsumer> logger)
    {
        _logger = logger;
    }

    public Task Consume(ConsumeContext<PackageDeletedEvent> context)
    {
        _logger.LogWarning("AUDIT: Package with id={PackageId} was deleted.", context.Message.PackageId);
        return Task.CompletedTask;
    }
}
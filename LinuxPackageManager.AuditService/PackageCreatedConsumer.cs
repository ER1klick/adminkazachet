using LinuxPackageManager.Events;
using MassTransit;
using Microsoft.Extensions.Logging;

namespace LinuxPackageManager.AuditService;

public class PackageCreatedConsumer : IConsumer<PackageCreatedEvent>
{
    private readonly ILogger<PackageCreatedConsumer> _logger;
    
    public PackageCreatedConsumer(ILogger<PackageCreatedConsumer> logger)
    {
        _logger = logger;
    }


    public Task Consume(ConsumeContext<PackageCreatedEvent> context)
    {
        var eventMessage = context.Message;
        
        if (eventMessage.PackageName.Equals("CRASH", StringComparison.OrdinalIgnoreCase))
        {
            _logger.LogError("Simulating processing error for package '{PackageName}'. Sending to DLQ.", eventMessage.PackageName);
            throw new InvalidOperationException("This is a simulated crash!");
        }

        _logger.LogInformation("AUDIT: Received new package event: PackageId={PackageId}, Name={PackageName}",
            eventMessage.PackageId,
            eventMessage.PackageName);
    
        return Task.CompletedTask;
        // TODO: Здесь могла бы быть логика аудита, отправки email и т.д.
    }
}
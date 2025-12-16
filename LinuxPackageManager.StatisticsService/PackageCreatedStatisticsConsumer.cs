using LinuxPackageManager.Events;
using MassTransit;
using Microsoft.Extensions.Logging;

namespace LinuxPackageManager.StatisticsService;

public class PackageCreatedStatisticsConsumer : IConsumer<PackageCreatedEvent>
{
    private readonly ILogger<PackageCreatedStatisticsConsumer> _logger;
    private static int _totalPackagesCreated = 0;

    public PackageCreatedStatisticsConsumer(ILogger<PackageCreatedStatisticsConsumer> logger)
    {
        _logger = logger;
    }
    
    public Task Consume(ConsumeContext<PackageCreatedEvent> context)
    {
        Interlocked.Increment(ref _totalPackagesCreated);

        _logger.LogInformation(
            "STATISTICS: New package created! Total packages so far: {TotalCount}",
            _totalPackagesCreated);
        
        return Task.CompletedTask;
    }
}
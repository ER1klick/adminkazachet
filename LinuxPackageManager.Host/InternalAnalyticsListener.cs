using LinuxPackageManager.Events;
using MassTransit;

namespace PackageManager.Host;


public class InternalAnalyticsListener : IConsumer<UserRatedEvent>
{
    private readonly ILogger<InternalAnalyticsListener> _logger;
    public InternalAnalyticsListener(ILogger<InternalAnalyticsListener> logger) => _logger = logger;
    public Task Consume(ConsumeContext<UserRatedEvent> context)
    {
        _logger.LogInformation("HOST INTERNAL: We just rated user {UserId}", context.Message.UserId);
        return Task.CompletedTask;
    }
}
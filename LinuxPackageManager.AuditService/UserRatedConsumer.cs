using LinuxPackageManager.Events;
using MassTransit;

namespace LinuxPackageManager.AuditService;

// В AuditService
public class UserRatedConsumer : IConsumer<UserRatedEvent>
{
    private readonly ILogger<UserRatedConsumer> _logger;
    public UserRatedConsumer(ILogger<UserRatedConsumer> logger) => _logger = logger;
    public Task Consume(ConsumeContext<UserRatedEvent> context)
    {
        _logger.LogInformation("AUDIT (RATING): User {UserId} has new rating: {Score}",
            context.Message.UserId, context.Message.Score);
        return Task.CompletedTask;
    }
}
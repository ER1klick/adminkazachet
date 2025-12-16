using LinuxPackageManager.Events;
using LinuxPackageManager.NotificationService.Hubs;
using MassTransit;
using Microsoft.AspNetCore.SignalR;

namespace LinuxPackageManager.NotificationService.Consumers;

public class NotificationUserRatedConsumer : IConsumer<UserRatedEvent>
{
    private readonly ILogger<NotificationUserRatedConsumer> _logger;
    private readonly IHubContext<NotificationHub> _hubContext;
    
    public NotificationUserRatedConsumer(
        ILogger<NotificationUserRatedConsumer> logger, 
        IHubContext<NotificationHub> hubContext)
    {
        _logger = logger;
        _hubContext = hubContext;
    }

    public async Task Consume(ConsumeContext<UserRatedEvent> context)
    {
        var message = context.Message;
        _logger.LogInformation("NOTIFICATION: Sending alert for user {UserId} with score {Score}", 
            message.UserId, message.Score);
        
        await _hubContext.Clients.All.SendAsync("ReceiveRatingNotification", new
        {
            userId = message.UserId,
            score = message.Score,
            verdict = message.Verdict
        });
    }
}
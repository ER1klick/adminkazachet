using Microsoft.AspNetCore.SignalR;
using Microsoft.Extensions.Logging;
using System.Threading.Tasks;

namespace LinuxPackageManager.NotificationService.Hubs;

public class NotificationHub : Hub
{
    private readonly ILogger<NotificationHub> _logger;

    public NotificationHub(ILogger<NotificationHub> logger)
    {
        _logger = logger;
    }

    public override Task OnConnectedAsync()
    {
        _logger.LogInformation("New SignalR connection: id={ConnectionId}", Context.ConnectionId);
        return base.OnConnectedAsync();
    }

    public override Task OnDisconnectedAsync(System.Exception? exception)
    {
        _logger.LogInformation("SignalR connection closed: id={ConnectionId}, reason={Exception}", 
            Context.ConnectionId, exception?.Message);
        return base.OnDisconnectedAsync(exception);
    }
}
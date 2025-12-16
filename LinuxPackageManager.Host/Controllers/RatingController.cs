using LinuxPackageManager.Analytics.Grpc;
using LinuxPackageManager.Events;
using MassTransit;
using Microsoft.AspNetCore.Mvc;

namespace LinuxPackageManager.Host.Controllers;

[ApiController]
public class RatingController : ControllerBase
{
    private readonly AnalyticsService.AnalyticsServiceClient _analyticsClient;
    private readonly IPublishEndpoint _publishEndpoint;

    public RatingController(AnalyticsService.AnalyticsServiceClient analyticsClient, IPublishEndpoint publishEndpoint)
    {
        _analyticsClient = analyticsClient;
        _publishEndpoint = publishEndpoint;
    }

    [HttpPost("/api/users/{id:long}/rate")]
    public async Task<IActionResult> RateUser([FromRoute] long id)
    {
        var request = new UserRatingRequest { UserId = id, Category = "General" };
        var grpcResponse = await _analyticsClient.CalculateUserRatingAsync(request);

        var anEvent = new UserRatedEvent(grpcResponse.UserId, grpcResponse.RatingScore, grpcResponse.Verdict);
        await _publishEndpoint.Publish(anEvent);
        
        return Ok($"Rating calculated: {grpcResponse.RatingScore}. Event published.");
    }
}
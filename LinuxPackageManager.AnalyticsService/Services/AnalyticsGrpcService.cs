
namespace LinuxPackageManager.AnalyticsService.Services;

public class AnalyticsGrpcService : LinuxPackageManager.Analytics.Grpc.AnalyticsService.AnalyticsServiceBase
{
    public override System.Threading.Tasks.Task<LinuxPackageManager.Analytics.Grpc.UserRatingResponse> CalculateUserRating(
        LinuxPackageManager.Analytics.Grpc.UserRatingRequest request, 
        Grpc.Core.ServerCallContext context)
    {
        var score = new System.Random().Next(0, 101);
        var verdict = score > 50 ? "GOOD" : "BAD";

        var response = new LinuxPackageManager.Analytics.Grpc.UserRatingResponse
        {
            UserId = request.UserId,
            RatingScore = score,
            Verdict = verdict
        };
        
        return System.Threading.Tasks.Task.FromResult(response);
    }
}
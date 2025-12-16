namespace LinuxPackageManager.Events;
public record UserRatedEvent(long UserId, int Score, string Verdict);
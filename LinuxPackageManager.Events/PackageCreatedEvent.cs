namespace LinuxPackageManager.Events;

public record PackageCreatedEvent(
    long PackageId,
    string PackageName,
    string Version
);
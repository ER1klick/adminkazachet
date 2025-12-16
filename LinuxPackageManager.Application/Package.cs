using System.ComponentModel.DataAnnotations;

namespace LinuxPackageManager.Application; // Или Domain

public class Package
{
    public long Id { get; set; }

    [Required]
    public string Name { get; set; }

    [Required]
    public string Version { get; set; }

    [Required]
    public string Architecture { get; set; }

    public string RepositoryName { get; set; }
    
    public DateTime CreatedAt { get; set; }
}
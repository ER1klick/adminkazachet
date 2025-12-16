namespace LinuxPackageManager.Api.Contracts.Dto;
using System.ComponentModel.DataAnnotations;

public record CreatePackageRequest(
    [Required(ErrorMessage = "Packet name can't be empty")]
    [MinLength(2, ErrorMessage = "Packet name can't be less than 2")]
    string Name,
    
    [Required(ErrorMessage = "Version can't be empty")]
    string Version,
    
    [Required(ErrorMessage = "Architecture can't be empty")]
    string Architecture,
    
    [Required(ErrorMessage = "Repository ID can't be empty")]
    long RepositoryId
    
    );
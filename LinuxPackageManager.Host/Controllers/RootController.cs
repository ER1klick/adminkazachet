using LinuxPackageManager.Api.Contracts.EndPoints;
using LinuxPackageManager.Api.Contracts.Hateoas;
using Microsoft.AspNetCore.Mvc;

namespace LinuxPackageManager.Host.Controllers;

[ApiController]
[Route("/api")]
public class RootController : ControllerBase
{
    [HttpGet(Name = "GetRoot")]
    public IActionResult GetRoot()
    {
        var root = new RootResponse();

        root.Links.Add(
            new LinkDto(
                Url.Link(nameof(IPackagesApi.GetAllPackages), null), 
                "packages", 
                "GET"));
        
        return Ok(root);
    }
}
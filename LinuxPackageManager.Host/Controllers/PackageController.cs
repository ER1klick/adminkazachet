using LinuxPackageManager.Api.Contracts.Dto;
using LinuxPackageManager.Api.Contracts.EndPoints;
using LinuxPackageManager.Application;
using Microsoft.AspNetCore.Mvc;
using System.Net.Mime;

namespace LinuxPackageManager.Host.Controllers;

[ApiController]
[Route("/api/packages")]
[Produces(MediaTypeNames.Application.Json)]
public class PackagesController : ControllerBase, IPackagesApi
{
    private readonly IPackageService _packageService;
    private readonly PackageResourceAssembler _assembler;

    public PackagesController(IPackageService packageService, PackageResourceAssembler assembler)
    {
        _packageService = packageService;
        _assembler = assembler;
    }
    
    [HttpGet("{id:long}")]
    public async Task<IActionResult> GetPackageById([FromRoute] long id)
    {
        var package = await _packageService.GetByIdAsync(id);

        if (package is null)
        {
            return NotFound(new StatusResponse("error", $"Пакет с ID {id} не найден"));
        }
        
        var resource = _assembler.ToResource(package);
        
        return Ok(resource);
    }

    [HttpPost]
    public async Task<IActionResult> CreatePackage([FromBody] CreatePackageRequest request)
    {
        var newPackage = await _packageService.CreateAsync(request);
        
        var resource = _assembler.ToResource(newPackage);
        
        return CreatedAtAction(nameof(GetPackageById), new { id = newPackage.Id }, resource);
    }
    [HttpGet(Name = nameof(IPackagesApi.GetAllPackages))] 
    public async Task<IActionResult> GetAllPackages([FromQuery] int pageNumber = 1, [FromQuery] int pageSize = 10)
    {
        var pagedList = await _packageService.GetAllAsync(pageNumber, pageSize);
        
        var resource = _assembler.ToCollectionResource(pagedList, nameof(IPackagesApi.GetAllPackages));
        
        return Ok(resource);
    }
    
    [HttpDelete("{id:long}", Name = nameof(IPackagesApi.DeletePackage))]
    public async Task<IActionResult> DeletePackage([FromRoute] long id)
    {
        var success = await _packageService.DeleteAsync(id);
        if (!success)
        {
            return NotFound();
        }
        return NoContent();
    }
    
}
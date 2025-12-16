using LinuxPackageManager.Api.Contracts.Dto;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc; // Все еще нужен для типа IActionResult
using Swashbuckle.AspNetCore.Annotations;

namespace LinuxPackageManager.Api.Contracts.EndPoints;

public interface IPackagesApi
{
    [SwaggerOperation(Summary = "Получить пакет по ID")]
    [SwaggerResponse(StatusCodes.Status200OK, "Пакет найден", typeof(PackageResponse))]
    [SwaggerResponse(StatusCodes.Status404NotFound, "Пакет не найден", typeof(StatusResponse))]
    Task<IActionResult> GetPackageById(long id);
    
    [SwaggerOperation(Summary = "Создать новый пакет")]
    [SwaggerResponse(StatusCodes.Status201Created, "Пакет успешно создан", typeof(PackageResponse))]
    [SwaggerResponse(StatusCodes.Status400BadRequest, "Невалидный запрос", typeof(StatusResponse))]
    Task<IActionResult> CreatePackage(CreatePackageRequest request);
    
    [HttpGet(Name = nameof(GetAllPackages))] 
    Task<IActionResult> GetAllPackages([FromQuery] int pageNumber = 1, [FromQuery] int pageSize = 10);
    
    [SwaggerOperation(Summary = "Удалить пакет по ID")]
    [SwaggerResponse(StatusCodes.Status204NoContent, "Пакет успешно удален")]
    [SwaggerResponse(StatusCodes.Status404NotFound, "Пакет не найден")]
    Task<IActionResult> DeletePackage([FromRoute] long id);
}
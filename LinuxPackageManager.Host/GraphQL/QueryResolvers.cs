using HotChocolate;
using LinuxPackageManager.Api.Contracts.Dto;
using LinuxPackageManager.Application;
using Microsoft.EntityFrameworkCore;
namespace LinuxPackageManager.Host.GraphQL;

public class QueryResolvers
{
    public async Task<List<PackageResponse>> Packages([Service] IPackageService packageService)
    {
        var pagedResult = await packageService.GetAllAsync(1, int.MaxValue); // Получаем все
        return pagedResult.Items;
    }

    public async Task<PackageResponse?> PackageById([Service] IPackageService packageService, long id)
    {
        return await packageService.GetByIdAsync(id);
    }
}
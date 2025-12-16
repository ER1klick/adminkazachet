using HotChocolate;
using LinuxPackageManager.Api.Contracts.Dto;
using LinuxPackageManager.Application;

namespace LinuxPackageManager.Host.GraphQL;

public class MutationResolvers
{
    public async Task<PackageResponse> CreatePackage(
        [Service] IPackageService packageService, 
        CreatePackageInput input)
    {
        var requestDto = new CreatePackageRequest(
            input.Name, 
            input.Version, 
            input.Architecture, 
            input.RepositoryId);

        return await packageService.CreateAsync(requestDto);
    }
}

public record CreatePackageInput(string Name, string Version, string Architecture, int RepositoryId);
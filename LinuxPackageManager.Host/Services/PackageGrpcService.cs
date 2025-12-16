using Grpc.Core; 
using LinuxPackageManager.Application;
using GrpcPackageService = LinuxPackageManager.Grpc.PackageService;
using LinuxPackageManager.Grpc;
using Google.Protobuf.WellKnownTypes;

namespace LinuxPackageManager.Host.Services;

public class PackageGrpcService : GrpcPackageService.PackageServiceBase
{
    private readonly IPackageService _packageService;

    public PackageGrpcService(IPackageService packageService)
    {
        _packageService = packageService;
    }
    
    public override async Task<PackageModel> GetPackageById(GetPackageByIdRequest request, ServerCallContext context)
    {
        var packageDto = await _packageService.GetByIdAsync(request.Id);
        if (packageDto is null)
        {
            throw new RpcException(new Status(StatusCode.NotFound, $"Package with ID {request.Id} not found."));
        }

        return new PackageModel
        {
            Id = packageDto.Id,
            Name = packageDto.Name,
            Version = packageDto.Version,
            Architecture = packageDto.Architecture,
            RepositoryName = packageDto.RepositoryName,
            CreatedAt = Timestamp.FromDateTime(packageDto.CreatedAt.ToUniversalTime())
        };
    }
    
    public override async Task<PackageModel> CreatePackage(CreatePackageRequest request, ServerCallContext context)
    {
        var createDto = new Api.Contracts.Dto.CreatePackageRequest(
            request.Name,
            request.Version,
            request.Architecture,
            request.RepositoryId
        );
        
        var createdPackageDto = await _packageService.CreateAsync(createDto);
        
        return new PackageModel
        {
            Id = createdPackageDto.Id,
            Name = createdPackageDto.Name,
            Version = createdPackageDto.Version,
            Architecture = createdPackageDto.Architecture,
            RepositoryName = createdPackageDto.RepositoryName,
            CreatedAt = Timestamp.FromDateTime(createdPackageDto.CreatedAt.ToUniversalTime())
        };
    }
}
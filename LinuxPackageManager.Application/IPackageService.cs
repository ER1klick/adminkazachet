using LinuxPackageManager.Api.Contracts.Dto;

namespace LinuxPackageManager.Application;

public interface IPackageService
{
    Task<PackageResponse?>GetByIdAsync(long id);
    
    Task<PackageResponse?> CreateAsync(CreatePackageRequest request);
    Task<PagedList<PackageResponse>> GetAllAsync(int pageNumber, int pageSize);
    
    Task<bool> DeleteAsync(long id);
}
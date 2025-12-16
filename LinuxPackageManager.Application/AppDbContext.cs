using LinuxPackageManager.Api.Contracts.Dto;
using Microsoft.EntityFrameworkCore;

namespace LinuxPackageManager.Application;

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
    {
    }

    public DbSet<Package> Packages { get; set; }
}
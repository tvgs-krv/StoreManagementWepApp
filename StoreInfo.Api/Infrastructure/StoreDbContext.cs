using Microsoft.EntityFrameworkCore;
using StoreInfo.Api.Application.Interfaces;
using StoreInfo.Api.Application.Models;
using StoreInfo.Api.Infrastructure.EntityTypeConfigurations;

namespace StoreInfo.Api.Infrastructure;

public class StoreDbContext : DbContext, IStoreDbContext
{
    public DbSet<StoreItem> StoreItems { get; set; }

    public StoreDbContext(DbContextOptions<StoreDbContext> options)
        : base(options) { }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        builder.ApplyConfiguration(new StoreConfiguration());
        base.OnModelCreating(builder);
    }

}
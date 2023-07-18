using Microsoft.EntityFrameworkCore;
using StoreInfo.Api.Application.Models;

namespace StoreInfo.Api.Application.Interfaces;

public interface IStoreDbContext
{
    DbSet<StoreItem> StoreItems { get; set; }

    Task<int> SaveChangesAsync(CancellationToken cancellationToken);
}
using Microsoft.EntityFrameworkCore;
using Services.Common.Exceptions;
using StoreInfo.Api.Application.Interfaces;
using StoreInfo.Api.Application.Models;
using StoreInfo.Api.Application.Queries.ViewModels;

namespace StoreInfo.Api.Application.Queries;

public class StoryQueries : IStoryQueries
{
    private readonly IStoreDbContext _dbContext;

    public StoryQueries(IStoreDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<PaginatedItemsVm<StoreItem>> GetAllStoresAsync(int pageSize, int pageIndex, CancellationToken cancellationToken)
    {
        var totalItems = await _dbContext.StoreItems
            .LongCountAsync(cancellationToken);

        var itemsOnPage = await _dbContext.StoreItems
            .Include(b => b.ManagerInfo)
            .OrderBy(c => c.Name)
            .Skip(pageSize * pageIndex)
            .Take(pageSize)
            .ToListAsync(cancellationToken);

        return new PaginatedItemsVm<StoreItem>(pageIndex, pageSize, totalItems, itemsOnPage);
    }

    public async Task<PaginatedItemsVm<StoreItem>> GetStoresByNameAsync(string name, int pageSize, int pageIndex, CancellationToken cancellationToken)
    {
        var totalItems = await _dbContext.StoreItems
            .LongCountAsync(cancellationToken);

        var itemsOnPage = await _dbContext.StoreItems
            .Include(b => b.ManagerInfo)
            .Where(s=>s.Name.Contains(name))
            .OrderBy(c => c.Name)
            .Skip(pageSize * pageIndex)
            .Take(pageSize)
            .ToListAsync(cancellationToken);

        return new PaginatedItemsVm<StoreItem>(pageIndex, pageSize, totalItems, itemsOnPage);
    }

    public async Task<StoreItem> GetStoreItemAsync(Guid id)
    {
        var storeItem = await _dbContext.StoreItems
            .Include(b => b.ManagerInfo)
            .FirstOrDefaultAsync(si=>si.Id == id);
        
        if (storeItem == null)
            throw new NotFoundException($"По переданному id = {id} не удалось найти запись");

        return storeItem;
    }
}
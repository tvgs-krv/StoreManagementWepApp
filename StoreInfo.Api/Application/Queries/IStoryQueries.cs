using StoreInfo.Api.Application.Models;
using StoreInfo.Api.Application.Queries.ViewModels;

namespace StoreInfo.Api.Application.Queries;

public interface IStoryQueries
{
    public Task<PaginatedItemsVm<StoreItem>> GetAllStoresAsync(int pageSize, int pageIndex, CancellationToken cancellationToken);

    public Task<PaginatedItemsVm<StoreItem>> GetStoresByNameAsync(string name, int pageSize, int pageIndex, CancellationToken cancellationToken);

    public Task<StoreItem> GetStoreItemAsync(Guid id);
}
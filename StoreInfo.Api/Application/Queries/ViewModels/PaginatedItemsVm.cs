using MediatR;
using StoreInfo.Api.Application.Models;

namespace StoreInfo.Api.Application.Queries.ViewModels;

public class PaginatedItemsVm<TEntity> : IRequest<Unit>, IRequest<PaginatedItemsVm<StoreItem>> where TEntity : class
{
    public int PageIndex { get; }

    public int PageSize { get; }

    public long Count { get; }

    public IEnumerable<TEntity> Data { get; }

    public PaginatedItemsVm(int pageIndex, int pageSize, long count, IEnumerable<TEntity> data)
    {
        PageIndex = pageIndex;
        PageSize = pageSize;
        Count = count;
        Data = data;
    }
}

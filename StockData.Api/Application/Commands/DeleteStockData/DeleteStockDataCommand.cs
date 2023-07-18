using MediatR;

namespace StockData.Api.Application.Commands.DeleteStockData;

public class DeleteStockDataCommand : IRequest<Unit>
{
    public Guid StoreItemId { get; set; }
}
using MediatR;

namespace StockData.Api.Application.Commands.CreateStockData;

public class CreateStockDataCommand : IRequest<Guid>
{
    public Guid? Id { get; set; }

    public Guid StoreItemId { get; set; }

    public int Backstory { get; set; }

    public int Frontstore { get; set; }

    public int ShoppingWindow { get; set; }

    public double Accuracy { get; set; }

    public double OnFloorAvailability { get; set; }

    public int MeanAge { get; set; }
}
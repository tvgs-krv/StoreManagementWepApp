using System.ComponentModel.DataAnnotations;

namespace StockData.Api.Application.Models;

public class StockData
{
    [Key]
    public Guid Id { get; set; }

    public Guid StoreItemId { get; set; }

    public int Backstory { get; set; }

    public int Frontstore { get; set; }

    public int ShoppingWindow { get; set; }

    public double TotalStock { get; set; }

    public double Accuracy { get; set; }

    public double OnFloorAvailability { get; set; }

    public int MeanAge { get; set; }

    public DateTime CreatedOn { get; set; }

    public DateTime ModifiedOn { get; set; }

}
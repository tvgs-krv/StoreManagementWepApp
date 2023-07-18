namespace StockData.Api.Application.Models;

public class StockDataSum
{
    public double MinTotalStock { get; set; }
    public double MaxTotalStock { get; set; }
    public double MedTotalStock { get; set; }

    public double MinAccuracy { get; set; }
    public double MaxAccuracy { get; set; }
    public double MedAccuracy { get; set; }

    public double MinOnFloorAvailability { get; set; }
    public double MaxOnFloorAvailability { get; set; }
    public double MedOnFloorAvailability { get; set; }

    public double MinMeanAge { get; set; }
    public double MaxMeanAge { get; set; }
    public double MedMeanAge { get; set; }

}
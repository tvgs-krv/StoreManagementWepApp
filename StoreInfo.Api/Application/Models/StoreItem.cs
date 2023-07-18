namespace StoreInfo.Api.Application.Models;

public class StoreItem
{
    public Guid Id { get; set; }

    public string Name { get; set; }

    public string CountryCode { get; set; }

    public string Email { get; set; }

    public int? Category { get; set; }

    public ManagerInfo ManagerInfo { get; set; }

    public DateTime CreatedOn { get; set; }

    public DateTime ModifiedOn { get; set; }
}



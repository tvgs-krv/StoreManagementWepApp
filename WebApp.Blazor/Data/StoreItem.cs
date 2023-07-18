using System.ComponentModel.DataAnnotations;

namespace WebApp.Blazor.Data;

public class StoreItem
{
    [Required]
    public Guid Id { get; set; }

    [Required]
    public string Name { get; set; }

    [Required]
    public string CountryCode { get; set; }

    [Required]
    public string Email { get; set; }

    [Required]
    public int Category { get; set; }

    [Required]
    public ManagerInfo ManagerInfo { get; set; }
}



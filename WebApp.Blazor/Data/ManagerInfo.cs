using System.ComponentModel.DataAnnotations;

namespace WebApp.Blazor.Data;

public class ManagerInfo
{
    [Required]
    public Guid Id { get; set; }

    [Required]
    public string FirstName { get; set; }

    [Required]
    public string LastName { get; set; }

    [Required]
    public string Email { get; set; }
}
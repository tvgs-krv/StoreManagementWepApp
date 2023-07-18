using System.ComponentModel.DataAnnotations.Schema;

namespace StoreInfo.Api.Application.Models;

public class ManagerInfo
{
    public Guid Id { get; set; }

    public string FirstName { get; set; }

    public string LastName { get; set; }

    public string Email { get; set; }
}
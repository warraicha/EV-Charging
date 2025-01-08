using System.ComponentModel.DataAnnotations;

namespace EVChargingAPI.Models;

public class Address
{
    [Required]
    public string Line1 { get; set; }

    public string? Line2 { get; set; }

    [Required]
    public string City { get; set; }

    public string? County { get; set; }

    [Required]
    public string Postcode { get; set; }
}
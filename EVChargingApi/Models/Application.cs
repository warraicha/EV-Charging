using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace EVChargingAPI.Models;

public class Application
{
    [Key]
    public Guid Id { get; set; }

    [Required]
    public string Name { get; set; }

    [Required]
    [EmailAddress]
    public string Email { get; set; }

    [Required]
    public Address Address { get; set; }

    [Required]
    public string VehicleRegistrationNumber { get; set; }

    [JsonIgnore]
    public DateTime? Timestamp { get; set; }
}
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace EVChargingAPI.Models;

public class Application
{
    [Key]
    public Guid Id { get; set; }

    public string? FirstName { get; set; }

    public string? LastName { get; set; }

    [EmailAddress]
    public string? Email { get; set; }

    public Address? Address { get; set; }

    public string? VehicleRegistrationNumber { get; set; }

    [JsonIgnore]
    public DateTime? Timestamp { get; set; }
}
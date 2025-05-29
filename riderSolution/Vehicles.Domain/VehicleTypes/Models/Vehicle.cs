using System.ComponentModel.DataAnnotations;

namespace Vehicles.Domain.VehicleTypes.Models;

public abstract class Vehicle
{
    public int Id { get; init; }
    
    [MaxLength(50)]
    [Required]
    public required string Brand { get; set; }
    
    [MaxLength(50)]
    [Required]
    public required string Model { get; set; }
    
    [MaxLength(50)]
    [Required]
    public required string TransmissionType { get; set; }
    
    [MaxLength(50)]
    [Required]
    public required string FuelType { get; set; }
    
    [MaxLength(30)]
    [Required]
    public required string Color { get; set; }
    
    [Required]
    public required int Year { get; set; }
    
    [Required]
    public required int EnginePower { get; set; }
    
    [Required]
    public required int Mileage { get; set; }
    
    [Required]
    public required int MaxSpeed { get; set; }
    
    [Required]
    public required double EngineVolume { get; set; }
    
    [Required]
    public required double FuelConsumption { get; set; }
    
}
using System.ComponentModel.DataAnnotations;

namespace Vehicles.Domain.VehicleTypes.Models.VehicleModels;

public class Year
{
    public int Id { get; set; }

    private int _year;

    [Required]
    public int YearNum
    {
        get => _year;
        set
        {
            if (value > DateTime.Now.Year) throw new ArgumentException("Year cannot exceed DateTime.Now year.");
            
            _year = value;
        }
    }
    
    public virtual ICollection<Model> Models { get; set; } = new List<Model>();
}
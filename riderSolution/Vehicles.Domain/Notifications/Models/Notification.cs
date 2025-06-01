using System.ComponentModel.DataAnnotations;

namespace Vehicles.Domain.Notifications.Models;

public class Notification
{
    public int Id { get; set; }

    private string _title;

    [Required]
    [MaxLength(150)]
    public string Title
    {
        get => _title;
        set
        {
            if (string.IsNullOrWhiteSpace(value)) throw new ArgumentException("Title type is required.");
            if (value.Length > 150) throw new ArgumentException("Title type cannot exceed 150 characters.");
            
            _title = value;
        }
    }

    private string _body;

    [Required]
    public string Body
    {
        get => _body;
        set
        {
            if (string.IsNullOrWhiteSpace(value)) throw new ArgumentException("Body type is required.");
            
            _body = value;
        }
    }

    private DateTime _date;

    [Required]
    public DateTime Date
    {
        get => _date;
        set
        {
            if (value > DateTime.Now) throw new ArgumentException("Date cannot be in the future.");
            
            _date = value;
        }
    }
}
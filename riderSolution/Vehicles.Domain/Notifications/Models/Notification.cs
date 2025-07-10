using System.ComponentModel.DataAnnotations;

namespace Vehicles.Domain.Notifications.Models;

public class Notification : BaseEntity
{
    private string _title;

    [Required]
    [MaxLength(150)]
    public string Title
    {
        get => _title;
        set
        {
            if (string.IsNullOrWhiteSpace(value)) throw new ArgumentException("Title is required.");
            if (value.Length > 150) throw new ArgumentException("Title cannot exceed 150 characters.");
            
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
            if (string.IsNullOrWhiteSpace(value)) throw new ArgumentException("Body is required.");
            
            _body = value;
        }
    }

    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

    public bool IsSent { get; set; } = false;

    public bool IsRead { get; set; } = false;
}
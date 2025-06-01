using System.ComponentModel.DataAnnotations;
using System.Net.Mail;
using Vehicles.Domain.Posts.Models;

namespace Vehicles.Domain.Users.Models;

public class Admin
{
    public int Id { get; init; }

    private string _name;

    [Required]
    [MaxLength(30)]
    public string Name
    {
        get => _name;
        set
        {
            if (string.IsNullOrWhiteSpace(value)) throw new ArgumentException("Name is required.");
            if (value.Length > 50) throw new ArgumentException("Name cannot exceed 50 characters.");
            
            _name = value;
        }
    }

    private string _email;

    [Required]
    [EmailAddress]
    public string Email
    {
        get => _email;
        set
        {
            if (string.IsNullOrWhiteSpace(value)) throw new ArgumentException("Email is required.");
            
            try
            {
                var isEmail = new MailAddress(value);
            }
            catch
            {
                throw new ArgumentException("Invalid email format.");
            }
            
            _email = value;
        }
    }

    private string _password;

    [MaxLength(50)]
    [Required]
    public string Password
    {
        get => _password;
        set
        {
            if (string.IsNullOrWhiteSpace(value)) throw new ArgumentException("Password is required.");
            if (value.Length > 50) throw new ArgumentException("Password cannot exceed 50 characters.");
            
            _password = value;
        }
    }
}
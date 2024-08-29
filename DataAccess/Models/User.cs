using System.ComponentModel.DataAnnotations;

namespace DataAccess.Models;

public class User:BaseModel
{
    [MaxLength(32)]
    public required string Username { get; set; }
    [MaxLength(32)]
    public required string Password { get; set; }
    public UserRole Role { get; set; }
}
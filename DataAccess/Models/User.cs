namespace DataAccess.Models;

public class User:BaseModel
{
    public required string Username { get; set; }
    public required string Password { get; set; }
    public UserRole Role { get; set; }
}
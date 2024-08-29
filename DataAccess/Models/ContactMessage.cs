using System.ComponentModel.DataAnnotations;

namespace DataAccess.Models;

public class ContactMessage : BaseModel
{
    [MaxLength(320)] //maximum length of an email is 320 chars
    public string Email { get; set; } = string.Empty;
    [MaxLength(50)]
    public string Name { get; set; } = string.Empty;
    [MaxLength(150)]
    public string Subject { get; set; } = string.Empty;
    [MaxLength(1000)]
    public string Text { get; set; } = string.Empty;
}
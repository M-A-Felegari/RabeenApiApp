namespace DataAccess.Models;

public class ContactRequest : BaseModel
{
    public string Email { get; set; } = string.Empty;
    public string Name { get; set; } = string.Empty;
    public string Text { get; set; } = string.Empty;
}
using System.ComponentModel.DataAnnotations;

namespace DataAccess.Models;

public class Achievement : BaseModel
{
    [MaxLength(100)]
    public string Title { get; set; } = string.Empty;
    [MaxLength(400)]
    public string Description { get; set; } = string.Empty;
    [MaxLength(50)]
    public string ExtraInformation { get; set; } = string.Empty; //for example: 1402-1403 or 27-7-1394 etc.
    public Member Owner { get; set; }
}
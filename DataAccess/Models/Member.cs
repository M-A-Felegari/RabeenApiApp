using System.ComponentModel.DataAnnotations;

namespace DataAccess.Models;

public class Member : BaseModel
{
    [MaxLength(50)]
    public string Name { get; set; } = string.Empty;
    [MaxLength(50)]
    public string Title { get; set; } = string.Empty;
    public bool IsMainMember { get; set; }
    [MaxLength(450)]
    public string About { get; set; } = string.Empty;
    [MaxLength(150)]
    public string OwnPortfolio { get; set; } = string.Empty;

    public IEnumerable<Achievement>? Achievements { get; set; }
}
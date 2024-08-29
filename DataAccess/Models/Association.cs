using System.ComponentModel.DataAnnotations;

namespace DataAccess.Models;

public class Association : BaseModel
{
    [MaxLength(50)]
    public string Name { get; set; } = string.Empty;
    [MaxLength(50)]
    public string UniversityName { get; set; } = string.Empty;
    [MaxLength(100)]
    public string ContactLink { get; set; } = string.Empty;
    public DateTime CreationDate { get; set; }
    public IEnumerable<AssociationCooperation> Cooprations { get; set; } = new List<AssociationCooperation>();
}
using System.ComponentModel.DataAnnotations;

namespace DataAccess.Models;

public class AssociationCooperation : BaseModel
{
    [MaxLength(150)]
    public string Title { get; set; } = string.Empty;
    [MaxLength(650)]
    public string Description { get; set; } = string.Empty;
    public DateTime StartDate { get; set; }
    public DateTime FinishDate { get; set; }
    public Association Association { get; set; }
    public int AssociationId { get; set; }
}
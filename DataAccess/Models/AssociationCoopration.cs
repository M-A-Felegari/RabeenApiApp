namespace DataAccess.Models;

public class AssociationCoopration : BaseModel
{
    public string Title { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public DateTime StartDate { get; set; }
    public DateTime FinishDate { get; set; }
}
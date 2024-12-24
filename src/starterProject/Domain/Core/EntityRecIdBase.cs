using System.ComponentModel.DataAnnotations;

public class EntityRecIdBase
{
    [Key]
    public Guid RecId { get; set; }

    public bool Deleted { get; set; } = false;
}
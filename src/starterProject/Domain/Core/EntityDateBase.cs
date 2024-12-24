
public class EntityDateBase : EntityRecIdBase
{
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    public DateTime? DeletedDate { get; set; }
}
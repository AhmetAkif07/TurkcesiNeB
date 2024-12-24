
namespace Domain.Entities;

public class Customer : EntityDateBase
{
    public string Email { get; set; }
    public string Name { get; set; }
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
}

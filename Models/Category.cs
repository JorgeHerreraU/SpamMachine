using Microsoft.EntityFrameworkCore;

namespace SpamMachine.Models;

[Index(nameof(Category.Name), IsUnique = true)]
public class Category : BaseEntity
{
    public Category()
    {
        Messages = new HashSet<Message>();
    }
    public string Name { get; set; } = null!;
    public string? Description { get; set; } = null!;
    public virtual ICollection<Message> Messages { get; set; }
}
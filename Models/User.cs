using System.ComponentModel.DataAnnotations;
using Microsoft.EntityFrameworkCore;

namespace SpamMachine.Models;

[Index(nameof(Email), IsUnique = true)]

public class User : BaseEntity
{
    public User()
    {
        Messages = new HashSet<Message>();
    }

    [Display(Name = "First Name")]
    public string Firstname { get; set; } = null!;

    [Display(Name = "Last Name")]
    public string Lastname { get; set; } = null!;
    public string Email { get; set; } = null!;
    public virtual ICollection<Message> Messages { get; set; }

}
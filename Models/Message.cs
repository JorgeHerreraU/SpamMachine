using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace SpamMachine.Models;

public class Message : BaseEntity
{
    public Message()
    {
        Categories = new HashSet<Category>();
        Users = new HashSet<User>();
    }
    public string Subject { get; set; } = null!;
    public string Content { get; set; } = null!;
    public DateTime Schedule { get; set; }

    [Display(Name = "Is Sent?")]
    public bool IsSent { get; set; }
    public virtual ICollection<Category> Categories { get; set; }
    public virtual ICollection<User> Users { get; set; }

}
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using Microsoft.AspNetCore.Mvc.Rendering;
using SpamMachine.Models;

namespace SpamMachine.ViewModels;
public class MessageViewModel : BaseEntity
{
    [Required]
    public string Subject { get; set; } = null!;
    [Required]
    public string Content { get; set; } = null!;
    [Required]
    [DisplayFormat(DataFormatString = "{0:yyyy-MM-ddThh:mm}", ApplyFormatInEditMode = true)]
    [DataType(DataType.DateTime)]

    public DateTime Schedule { get; set; }

    [ValidateNever]
    public List<Category> Categories { get; set; } = null!;
    [ValidateNever]
    public List<Category> AllCategories { get; set; } = null!;
    public List<User> Users { get; set; } = null!;
}
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using SpamMachine.Models;
using System.ComponentModel.DataAnnotations;

namespace SpamMachine.ViewModels;

public class UserViewModel : BaseEntity
{
    [Display(Name = "First Name")]
    public string Firstname { get; set; } = null!;

    [Display(Name = "Last Name")]
    public string Lastname { get; set; } = null!;
    public string Email { get; set; } = null!;
    [Display(Name = "Subscribed Messages")]
    public List<string>? Messages { get; set; } = null!;
    [Display(Name = "Available Messages")]
    [ValidateNever]
    public List<Message> AllMessages { get; set; } = null!;
}

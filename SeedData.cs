using Microsoft.EntityFrameworkCore;
using SpamMachine.Data;
using SpamMachine.Models;

namespace SpamMachine;

public static class SeedData
{
    public static void Initialize(IServiceProvider serviceProvider)
    {
        using var context = new AppDbContext(serviceProvider.GetRequiredService<DbContextOptions<AppDbContext>>());
        context.Database.Migrate();
        if (context.Messages.Any()) return;

        context.Messages.AddRange(
            new Message
            {
                Subject = "Discount on the new product",
                Content = "We are offering a discount on the new product. Please visit our website.",
                Schedule = DateTime.Now.AddDays(1),
                IsSent = false,
                Categories = new HashSet<Category>
                {
                        new Category { Name = "Promotions", Description = "Deals available" },
                        new Category { Name = "Marketing", Description = "Marketing related" }
                },
                Users = new List<User>
                {
                        new User { Firstname = "John", Lastname = "Doe", Email = "johndoe@email.com" },
                        new User { Firstname = "Mary", Lastname = "Smith", Email = "marysmith@email.com" },
                        new User { Firstname = "Sam", Lastname = "Johnson", Email = "samjohnson@email.com" }
                }
            }
        );

        context.SaveChanges();
    }
}
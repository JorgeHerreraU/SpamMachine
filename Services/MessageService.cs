using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;
using SpamMachine.Data;
using SpamMachine.Models;

namespace SpamMachine.Services;

public class MessageService : IMessageService
{
    private readonly AppDbContext _context;

    public MessageService(AppDbContext context)
    {
        _context = context;
    }
    public async Task<Message?> Get(int id)
    {
        return await _context.Messages.Include(m => m.Users).Include(m => m.Categories).SingleOrDefaultAsync(m => m.Id == id);
    }

    public async Task<Message?> Get(Expression<Func<Message, bool>> predicate)
    {
        return await _context.Messages.Include(m => m.Users).Include(m => m.Categories).Where(predicate).SingleOrDefaultAsync();
    }


    public async Task<IEnumerable<Message>> GetAll()
    {
        return await _context.Messages.Include(m => m.Users).Include(m => m.Categories).ToListAsync();
    }

    public async Task<IEnumerable<Message>> GetAll(Expression<Func<Message, bool>> predicate)
    {
        return await _context.Messages.Include(m => m.Users).Include(m => m.Categories).Where(predicate).ToListAsync();
    }

    public async Task<Message> Create(Message message)
    {
        await UpsertUsersByEmail(message);
        await UpsertCategoriesByName(message);
        _context.Add(message);
        await _context.SaveChangesAsync();
        return message;
    }

    public async Task<Message> Update(Message message)
    {
        await UpsertUsersByEmail(message);
        await UpsertCategoriesByName(message);
        message.IsSent = message.Schedule <= DateTime.Now;
        _context.Update(message);
        await _context.SaveChangesAsync();
        return message;
    }

    private async Task UpsertUsersByEmail(Message message)
    {
        var users = await _context.Users.Where(x => message.Users.Select(y => y.Email).Contains(x.Email)).ToListAsync();
        foreach (var user in message.Users.Where(user => users.Select(x => x.Email).Contains(user.Email) == false))
            users.Add(user);
        message.Users = users;
    }

    private async Task UpsertCategoriesByName(Message message)
    {
        var categories = await _context.Categories.Where(x => message.Categories.Select(y => y.Name).Contains(x.Name)).ToListAsync();
        foreach (var category in message.Categories.Where(category => categories.Select(x => x.Name).Contains(category.Name) == false))
            categories.Add(category);
        message.Categories = categories;
    }

    public async Task<Message?> Delete(int id)
    {
        var message = await _context.Messages.FirstOrDefaultAsync(x => x.Id == id);
        if (message == null) return null;
        _context.Remove(message);
        await _context.SaveChangesAsync();
        return message;
    }


}
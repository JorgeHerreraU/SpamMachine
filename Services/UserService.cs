using Microsoft.EntityFrameworkCore;
using SpamMachine.Data;
using SpamMachine.Models;

namespace SpamMachine.Services;

public class UserService : IUserService
{
    private readonly AppDbContext _context;

    public UserService(AppDbContext context)
    {
        _context = context;
    }

    public async Task<User?> Get(int id)
    {
        return await _context.Users.Include(u => u.Messages).FirstOrDefaultAsync(u => u.Id == id);
    }

    public async Task<User?> Get(string email)
    {
        return await _context.Users.FirstOrDefaultAsync(x => x.Email == email);
    }

    public async Task<IEnumerable<User>> GetAll()
    {
        return await _context.Users.Include(u => u.Messages).ToListAsync();
    }

    public async Task<User> Create(User user)
    {
        if (await _context.Users.FirstOrDefaultAsync(x => x.Email == user.Email) == null)
        {
            _context.Add(user);
            await _context.SaveChangesAsync();
            return user;
        }

        return user;
    }

    public async Task<User> Update(User user)
    {
        _context.Update(user);
        await _context.SaveChangesAsync();
        return user;
    }

    public async Task<User?> Delete(int id)
    {
        var user = await _context.Users.FirstOrDefaultAsync(x => x.Id == id);
        if (user == null) return null;
        _context.Remove(user);
        await _context.SaveChangesAsync();
        return user;
    }

}
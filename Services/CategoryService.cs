using Microsoft.EntityFrameworkCore;
using SpamMachine.Data;
using SpamMachine.Models;

namespace SpamMachine.Services;

public class CategoryService : ICategoryService
{
    private readonly AppDbContext _context;

    public CategoryService(AppDbContext context)
    {
        _context = context;
    }

    public async Task<Category?> Get(int id)
    {
        return await _context.Categories.FindAsync(id);
    }

    public async Task<IEnumerable<Category>> GetAll()
    {
        return await _context.Categories.ToListAsync();
    }

    public async Task<Category> Create(Category category)
    {
        if (await _context.Categories.FirstOrDefaultAsync(x => x.Name == category.Name) == null)
        {
            _context.Add(category);
            await _context.SaveChangesAsync();
            return category;
        }

        return category;
    }

    public async Task<Category> Update(Category category)
    {
        if (await _context.Categories.FirstOrDefaultAsync(x => x.Name == category.Name) == null)
        {
            _context.Update(category);
            await _context.SaveChangesAsync();
            return category;
        }

        return category;
    }

    public async Task<Category?> Delete(int id)
    {
        var category = await _context.Categories.FirstOrDefaultAsync(x => x.Id == id);
        if (category == null) return null;
        _context.Remove(category);
        await _context.SaveChangesAsync();
        return category;
    }
}
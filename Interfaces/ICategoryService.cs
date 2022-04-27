using SpamMachine.Models;

namespace SpamMachine.Services;

public interface ICategoryService
{
    Task<Category?> Get(int id);
    Task<IEnumerable<Category>> GetAll();
    Task<Category> Create(Category category);
    Task<Category> Update(Category category);
    Task<Category?> Delete(int id);

}
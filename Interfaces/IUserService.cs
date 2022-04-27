using SpamMachine.Models;

namespace SpamMachine.Services;

public interface IUserService
{
    Task<User?> Get(int id);
    Task<User?> Get(string email);
    Task<IEnumerable<User>> GetAll();
    Task<User> Create(User user);
    Task<User> Update(User user);
    Task<User?> Delete(int id);

}
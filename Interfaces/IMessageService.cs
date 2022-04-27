using System.Linq.Expressions;
using SpamMachine.Models;

namespace SpamMachine.Services;

public interface IMessageService
{
    Task<Message?> Get(int id);
    Task<Message?> Get(Expression<Func<Message, bool>> predicate);
    Task<IEnumerable<Message>> GetAll();
    Task<IEnumerable<Message>> GetAll(Expression<Func<Message, bool>> predicate);
    Task<Message> Create(Message message);
    Task<Message> Update(Message message);
    Task<Message?> Delete(int id);

}
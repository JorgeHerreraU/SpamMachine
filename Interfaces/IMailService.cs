using Quartz;
using SpamMachine.Models;

namespace SpamMachine.Services;
public interface IMailService
{
    Task Send(Message message);
}
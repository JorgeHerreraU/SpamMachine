using Quartz;
using SpamMachine.Models;

namespace SpamMachine.Services;

public class MailService : IMailService
{
    public async Task Send(Message message)
    {
        await Console.Out.WriteAsync($"Sending message: {message.Subject}");
    }
}
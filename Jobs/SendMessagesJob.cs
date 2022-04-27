using System.Diagnostics;
using AutoMapper;
using Microsoft.AspNetCore.SignalR;
using Quartz;
using SpamMachine.DTOs;
using SpamMachine.Services;

namespace SpamMachine.Jobs;

public class SendMessagesJob : IJob
{
    private readonly IMapper _mapper;
    private readonly IHubContext<JobsHub> _hubContext;
    private readonly IMessageService _messageService;
    private readonly IMailService _mailService;

    public SendMessagesJob(IMapper mapper, IHubContext<JobsHub> hubContext, IMessageService messageService, IMailService mailService)
    {
        _mapper = mapper;
        _hubContext = hubContext;
        _messageService = messageService;
        _mailService = mailService;
    }
    public async Task Execute(IJobExecutionContext context)
    {
        var messages = await _messageService.GetAll(m => m.IsSent == false);
        foreach (var message in messages)
        {
            if (message.Schedule <= DateTime.Now)
            {
                await _mailService.Send(message);
                message.IsSent = true;
                await _messageService.Update(message);
            }
        }
        await _hubContext.Clients.All.SendAsync("ReceiveMessages", _mapper.Map<MessageQueueReadDTO[]>(messages).OrderBy(m => m.Schedule));
    }
}

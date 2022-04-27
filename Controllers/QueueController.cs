using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using SpamMachine.DTOs;
using SpamMachine.Services;

namespace SpamMachine.Controllers;

public class QueueController : Controller
{
    private readonly IMessageService _messageService;
    private readonly IMailService _mailService;
    private readonly IMapper _mapper;
    public QueueController(IMessageService messageService, IMailService mailService, IMapper mapper)
    {
        _messageService = messageService;
        _mailService = mailService;
        _mapper = mapper;
    }
    public async Task<IActionResult> Index()
    {
        var messages = await _messageService.GetAll(m => m.IsSent == false);
        return View(_mapper.Map<MessageQueueReadDTO[]>(messages).OrderBy(m => m.Schedule));
    }

    [HttpPost]
    public async Task<IActionResult> SendNow(int id)
    {
        var message = await _messageService.Get(id);
        if (message == null) return NotFound();
        message.Schedule = DateTime.Now;
        await _messageService.Update(message);
        return Json(new { success = true });
    }
}
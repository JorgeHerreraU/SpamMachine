#nullable disable
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using SpamMachine.Models;
using SpamMachine.Services;
using SpamMachine.ViewModels;

namespace SpamMachine.Controllers
{
    public class MessagesController : Controller
    {
        private readonly IMessageService _messageService;
        private readonly ICategoryService _categoryService;
        private readonly IMapper _mapper;

        public MessagesController(IMessageService messageService, ICategoryService categoryService, IMapper mapper)
        {
            _messageService = messageService;
            _categoryService = categoryService;
            _mapper = mapper;
        }

        // GET: Messages
        public async Task<IActionResult> Index() => View(await _messageService.GetAll());

        // GET: Messages/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null) return NotFound();
            var message = await _messageService.Get(id.Value);
            if (message == null) return NotFound();
            return View(message);
        }

        // GET: Messages/Create
        public async Task<IActionResult> Create()
        {
            var viewModel = _mapper.Map<MessageViewModel>(new Message());
            viewModel.AllCategories = (await _categoryService.GetAll()).ToList();
            return View(viewModel);
        }

        // POST: Messages/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Subject,Content,Schedule,Categories,IsSent,Users,Id")] MessageViewModel messageIn)
        {
            if (ModelState.IsValid)
            {
                await _messageService.Create(_mapper.Map<Message>(messageIn));
                return RedirectToAction(nameof(Index));
            }
            return View(messageIn);
        }

        // GET: Messages/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null) return NotFound();
            var message = await _messageService.Get(id.Value);
            if (message == null) return NotFound();
            var viewModel = _mapper.Map<MessageViewModel>(message);
            viewModel.AllCategories = (await _categoryService.GetAll()).ToList();
            return View(viewModel);
        }

        // POST: Messages/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Subject,Content,Schedule,Categories,IsSent,Users,Id")] MessageViewModel messageIn)
        {
            if (id != messageIn.Id) return NotFound();
            if (ModelState.IsValid)
            {
                try
                {
                    var message = await _messageService.Get(id);
                    _mapper.Map(messageIn, message);
                    await _messageService.Update(message);
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!MessageExists(messageIn.Id)) return NotFound();
                    else throw;
                }
                return RedirectToAction(nameof(Index));
            }
            return View(messageIn);
        }

        // GET: Messages/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null) return NotFound();
            var message = await _messageService.Get(id.Value);
            if (message == null) return NotFound();
            return View(message);
        }

        // POST: Messages/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var message = await _messageService.Get(id);
            await _messageService.Delete(id);
            return RedirectToAction(nameof(Index));
        }

        private bool MessageExists(int id) => _messageService.Get(id) != null;
    }
}

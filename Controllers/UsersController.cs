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
    public class UsersController : Controller
    {
        private readonly IUserService _userService;
        private readonly IMessageService _messageService;
        private readonly IMapper _mapper;

        public UsersController(IUserService userService, IMessageService messageService, IMapper mapper)
        {
            _userService = userService;
            _messageService = messageService;
            _mapper = mapper;
        }

        // GET: Users
        public async Task<IActionResult> Index() => View(await _userService.GetAll());

        // GET: Users/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null) return NotFound();
            var user = await _userService.Get(id.Value);
            if (user == null) return NotFound();
            return View(_mapper.Map<UserViewModel>(user));
        }

        // GET: Users/Create
        public IActionResult Create() => View();

        // POST: Users/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Firstname,Lastname,Email,Id")] User user)
        {
            if (ModelState.IsValid)
            {
                await _userService.Create(user);
                return RedirectToAction(nameof(Index));
            }
            return View(user);
        }

        // GET: Users/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null) return NotFound();
            var user = await _userService.Get(id.Value);
            if (user == null) return NotFound();
            var viewModel = _mapper.Map<UserViewModel>(user);
            viewModel.AllMessages = (await _messageService.GetAll()).ToList().Except(user.Messages).ToList();
            return View(viewModel);
        }

        // POST: Users/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Firstname,Lastname,Email,Messages,Id")] UserViewModel userIn)
        {
            if (id != userIn.Id) return NotFound();
            if (ModelState.IsValid)
            {
                try
                {
                    var user = await _userService.Get(id);
                    user.Messages.Clear();
                    _mapper.Map(userIn, user);
                    if (userIn.Messages != null)
                    {
                        foreach (var message in userIn.Messages)
                        {
                            user.Messages.Add(await _messageService.Get(m => m.Subject == message));
                        }
                    }
                    await _userService.Update(user);
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!UserExists(userIn.Id)) return NotFound();
                    else throw;
                }
                return Json(new { result = "Redirect", url = Url.Action("Index", "Users") });
            }
            return View(userIn);
        }

        // GET: Users/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null) return NotFound();
            var user = await _userService.Get(id.Value);
            if (user == null) return NotFound();
            return View(user);
        }

        // POST: Users/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var user = await _userService.Delete(id);
            if (user == null) return NotFound();
            return RedirectToAction(nameof(Index));
        }

        private bool UserExists(int id) => _userService.Get(id) != null;
    }
}

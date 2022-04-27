using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using SpamMachine.Models;

namespace SpamMachine.Controllers;

public class HomeController : Controller
{
    public HomeController() { }
    public IActionResult Index() => View();
    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error() => View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
}

using Microsoft.AspNetCore.Mvc;

namespace CareerOS.Controllers;

public class SettingsController : Controller
{
    public IActionResult Index() => View();
}

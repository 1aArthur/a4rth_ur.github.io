using CareerOS.Models;
using CareerOS.Services;
using Microsoft.AspNetCore.Mvc;

namespace CareerOS.Controllers;

public class GoalsController : Controller
{
    private readonly IGoalService _service;
    public GoalsController(IGoalService service) => _service = service;

    public async Task<IActionResult> Index() => View(await _service.GetAsync());

    [HttpPost]
    public async Task<IActionResult> Create(Goal model)
    {
        await _service.AddAsync(model);
        return RedirectToAction(nameof(Index));
    }
}

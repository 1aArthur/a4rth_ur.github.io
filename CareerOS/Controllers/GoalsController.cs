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
        if (!ModelState.IsValid) return RedirectToAction(nameof(Index));

        model.Nome = InputValidator.Safe(model.Nome, 120);
        model.Periodo = model.Periodo is "Semanal" or "Mensal" ? model.Periodo : "Semanal";
        model.Meta = Math.Clamp(model.Meta, 1, 10000);
        model.Atual = Math.Clamp(model.Atual, 0, 10000);

        await _service.AddAsync(model);
        return RedirectToAction(nameof(Index));
    }
}

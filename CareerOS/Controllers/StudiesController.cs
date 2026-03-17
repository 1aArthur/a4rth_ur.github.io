using CareerOS.Models;
using CareerOS.Services;
using Microsoft.AspNetCore.Mvc;

namespace CareerOS.Controllers;

public class StudiesController : Controller
{
    private readonly IStudyService _service;
    public StudiesController(IStudyService service) => _service = service;

    public async Task<IActionResult> Index() => View(await _service.GetSubjectsAsync());

    [HttpPost]
    public async Task<IActionResult> Create(Subject model)
    {
        if (!ModelState.IsValid) return RedirectToAction(nameof(Index));

        model.Nome = InputValidator.Safe(model.Nome, 80);
        model.Assunto = InputValidator.Safe(model.Assunto, 120);
        model.Subtopicos = InputValidator.Safe(model.Subtopicos, 400);

        await _service.AddSubjectAsync(model);
        return RedirectToAction(nameof(Index));
    }
}

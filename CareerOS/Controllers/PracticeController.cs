using CareerOS.Models;
using CareerOS.Services;
using Microsoft.AspNetCore.Mvc;

namespace CareerOS.Controllers;

public class PracticeController : Controller
{
    private readonly IPracticeService _service;
    public PracticeController(IPracticeService service) => _service = service;

    public async Task<IActionResult> Index() => View(await _service.GetAsync());

    [HttpPost]
    public async Task<IActionResult> Create(PracticeRecord model)
    {
        if (!ModelState.IsValid) return RedirectToAction(nameof(Index));

        model.Plataforma = InputValidator.Safe(model.Plataforma, 60);
        model.Titulo = InputValidator.Safe(model.Titulo, 120);
        model.Tema = InputValidator.Safe(model.Tema, 80);
        model.Linguagem = InputValidator.Safe(model.Linguagem, 30);
        model.Observacoes = InputValidator.Safe(model.Observacoes, 800);
        model.TempoMinutos = Math.Clamp(model.TempoMinutos, 1, 720);

        await _service.AddAsync(model);
        return RedirectToAction(nameof(Index));
    }
}

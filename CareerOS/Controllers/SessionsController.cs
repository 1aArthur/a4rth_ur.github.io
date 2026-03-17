using CareerOS.Models;
using CareerOS.Services;
using Microsoft.AspNetCore.Mvc;

namespace CareerOS.Controllers;

public class SessionsController : Controller
{
    private readonly IStudyService _service;
    public SessionsController(IStudyService service) => _service = service;

    public async Task<IActionResult> Index() => View(await _service.GetSessionsAsync());

    [HttpPost]
    public async Task<IActionResult> Create(StudySession model)
    {
        if (!ModelState.IsValid) return RedirectToAction(nameof(Index));

        model.Topico = InputValidator.Safe(model.Topico, 120);
        model.Observacoes = InputValidator.Safe(model.Observacoes, 800);
        model.DuracaoMinutos = Math.Clamp(model.DuracaoMinutos, 1, 720);
        model.NotaProdutividade = Math.Clamp(model.NotaProdutividade, 1, 5);
        model.Data = DateTime.Now;

        await _service.AddSessionAsync(model);
        return RedirectToAction(nameof(Index));
    }

    [HttpPost]
    public async Task<IActionResult> RegisterPomodoro(int minutos)
    {
        var safeMinutes = Math.Clamp(minutos, 1, 180);
        await _service.AddSessionAsync(new StudySession
        {
            Topico = "Pomodoro",
            DuracaoMinutos = safeMinutes,
            TipoSessao = SessionType.Revisao,
            Data = DateTime.Now,
            NotaProdutividade = 5
        });

        return Ok(new { message = "Sessão pomodoro registrada" });
    }
}

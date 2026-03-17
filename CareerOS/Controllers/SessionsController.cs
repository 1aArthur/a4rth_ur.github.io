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
        model.Data = DateTime.Now;
        await _service.AddSessionAsync(model);
        return RedirectToAction(nameof(Index));
    }

    [HttpPost]
    public async Task<IActionResult> RegisterPomodoro(int minutos)
    {
        await _service.AddSessionAsync(new StudySession { Topico = "Pomodoro", DuracaoMinutos = minutos, TipoSessao = SessionType.Revisao, Data = DateTime.Now, NotaProdutividade = 5 });
        return Ok(new { message = "Sessão pomodoro registrada" });
    }
}

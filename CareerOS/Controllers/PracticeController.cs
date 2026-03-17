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
        await _service.AddAsync(model);
        return RedirectToAction(nameof(Index));
    }
}

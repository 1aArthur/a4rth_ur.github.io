using CareerOS.Services;
using CareerOS.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace CareerOS.Controllers;

public class ProfessionalSummaryController : Controller
{
    private readonly IStudyService _studyService;
    private readonly IPracticeService _practiceService;
    private readonly IGitHubService _gitHubService;
    private readonly ISummaryService _summaryService;

    public ProfessionalSummaryController(IStudyService studyService, IPracticeService practiceService, IGitHubService gitHubService, ISummaryService summaryService)
    {
        _studyService = studyService;
        _practiceService = practiceService;
        _gitHubService = gitHubService;
        _summaryService = summaryService;
    }

    public IActionResult Index() => View(new SummaryPageViewModel());

    [HttpPost]
    public async Task<IActionResult> Generate(SummaryPageViewModel model)
    {
        var subjects = await _studyService.GetSubjectsAsync();
        var practices = await _practiceService.GetAsync();
        var repos = await _gitHubService.GetReposAsync(model.Username);
        model.Texto = _summaryService.Generate(subjects, practices, repos);
        return View("Index", model);
    }

    [HttpPost]
    public IActionResult ExportTxt(SummaryPageViewModel model)
    {
        var bytes = System.Text.Encoding.UTF8.GetBytes(model.Texto ?? string.Empty);
        return File(bytes, "text/plain", "career-summary.txt");
    }
}

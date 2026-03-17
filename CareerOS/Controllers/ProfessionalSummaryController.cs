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
        model.Username = InputValidator.Safe(model.Username, 39);

        var subjects = await _studyService.GetSubjectsAsync();
        var practices = await _practiceService.GetAsync();
        var repos = InputValidator.IsValidGitHubUsername(model.Username)
            ? await _gitHubService.GetReposAsync(model.Username)
            : [];

        model.Texto = _summaryService.Generate(subjects, practices, repos);
        return View("Index", model);
    }

    [HttpPost]
    public IActionResult ExportTxt(SummaryPageViewModel model)
    {
        var safeText = model.Texto?.Length > 5000 ? model.Texto[..5000] : (model.Texto ?? string.Empty);
        var bytes = System.Text.Encoding.UTF8.GetBytes(safeText);
        return File(bytes, "text/plain", "career-summary.txt");
    }
}

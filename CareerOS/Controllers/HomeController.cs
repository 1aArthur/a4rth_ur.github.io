using CareerOS.Models;
using CareerOS.Services;
using CareerOS.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace CareerOS.Controllers;

public class HomeController : Controller
{
    private readonly IStudyService _studyService;
    private readonly IPracticeService _practiceService;
    private readonly IGoalService _goalService;
    private readonly IGitHubService _gitHubService;

    public HomeController(IStudyService studyService, IPracticeService practiceService, IGoalService goalService, IGitHubService gitHubService)
    {
        _studyService = studyService;
        _practiceService = practiceService;
        _goalService = goalService;
        _gitHubService = gitHubService;
    }

    public async Task<IActionResult> Index()
    {
        var sessions = await _studyService.GetSessionsAsync();
        var subjects = await _studyService.GetSubjectsAsync();
        var practices = await _practiceService.GetAsync();
        var goals = await _goalService.GetAsync();
        var weekStart = DateTime.Today.AddDays(-(int)DateTime.Today.DayOfWeek + 1);

        var vm = new DashboardViewModel
        {
            HorasSemana = sessions.Where(x => x.Data >= weekStart).Sum(x => x.DuracaoMinutos) / 60,
            SessoesConcluidas = sessions.Count,
            MateriasAtivas = subjects.Count(x => x.Status == StudyStatus.EmProgresso),
            Pendencias = subjects.Count(x => x.Status != StudyStatus.Concluido),
            ExerciciosResolvidos = practices.Count(x => x.Status == ExerciseStatus.Resolvido),
            StreakDias = sessions.Select(x => x.Data.Date).Distinct().Count(),
            MetaPercentual = ComputeMeta(goals),
            ProgressoPorMateria = subjects.ToDictionary(x => x.Nome, x => x.NivelDominio)
        };

        var user = HttpContext.Session.GetString("github_user") ?? string.Empty;
        vm.UltimosRepos = (await _gitHubService.GetReposAsync(user)).Take(5).ToList();
        return View(vm);
    }

    private static int ComputeMeta(List<Goal> goals)
    {
        var horas = goals.FirstOrDefault(x => x.Periodo == "Semanal" && x.Nome.Contains("hora", StringComparison.OrdinalIgnoreCase));
        if (horas is null || horas.Meta == 0) return 0;
        return Math.Min(100, (int)((double)horas.Atual / horas.Meta * 100));
    }
}

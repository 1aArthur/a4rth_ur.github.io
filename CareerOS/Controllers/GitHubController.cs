using CareerOS.Services;
using CareerOS.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace CareerOS.Controllers;

public class GitHubController : Controller
{
    private readonly IGitHubService _gitHubService;
    public GitHubController(IGitHubService gitHubService) => _gitHubService = gitHubService;

    public async Task<IActionResult> Index(string username = "")
    {
        var vm = new GitHubPageViewModel { Username = username };
        if (!string.IsNullOrWhiteSpace(username))
        {
            vm.Repos = await _gitHubService.GetReposAsync(username);
            vm.Mensagem = vm.Repos.Count == 0 ? "Não foi possível carregar repositórios agora." : $"{vm.Repos.Count} repositórios carregados";
            HttpContext.Session.SetString("github_user", username);
        }
        return View(vm);
    }
}

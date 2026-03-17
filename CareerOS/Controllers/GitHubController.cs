using CareerOS.Services;
using CareerOS.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace CareerOS.Controllers;

public class GitHubController : Controller
{
    private readonly IGitHubService _gitHubService;
    private readonly IConfiguration _configuration;

    public GitHubController(IGitHubService gitHubService, IConfiguration configuration)
    {
        _gitHubService = gitHubService;
        _configuration = configuration;
    }

    public async Task<IActionResult> Index(string username = "")
    {
        var configured = _configuration["GitHub:DefaultUsername"] ?? string.Empty;
        var sessionUser = HttpContext.Session.GetString("github_user") ?? string.Empty;
        var userToUse = string.IsNullOrWhiteSpace(username) ? (string.IsNullOrWhiteSpace(sessionUser) ? configured : sessionUser) : username;

        var vm = new GitHubPageViewModel { Username = userToUse };

        if (!string.IsNullOrWhiteSpace(userToUse))
        {
            if (!InputValidator.IsValidGitHubUsername(userToUse))
            {
                vm.Mensagem = "Username do GitHub inválido.";
                return View(vm);
            }

            vm.Repos = await _gitHubService.GetReposAsync(userToUse);
            vm.Mensagem = vm.Repos.Count == 0
                ? "Não foi possível carregar repositórios agora."
                : $"{vm.Repos.Count} repositórios carregados para @{userToUse}";

            HttpContext.Session.SetString("github_user", InputValidator.NormalizeGitHubUsername(userToUse));
        }

        return View(vm);
    }
}

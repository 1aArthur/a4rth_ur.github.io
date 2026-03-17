using CareerOS.Services;
using Microsoft.AspNetCore.Mvc;

namespace CareerOS.Controllers;

public class SettingsController : Controller
{
    public IActionResult Index()
    {
        ViewData["GithubUsername"] = HttpContext.Session.GetString("github_user") ?? string.Empty;
        return View();
    }

    [HttpPost]
    public IActionResult SaveGithub(string githubUsername)
    {
        if (!InputValidator.IsValidGitHubUsername(githubUsername))
        {
            TempData["Message"] = "Username inválido.";
            return RedirectToAction(nameof(Index));
        }

        HttpContext.Session.SetString("github_user", InputValidator.NormalizeGitHubUsername(githubUsername));
        TempData["Message"] = "Username do GitHub salvo com segurança.";
        return RedirectToAction(nameof(Index));
    }
}

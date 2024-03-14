using Microsoft.AspNetCore.Mvc;

namespace Dily_Planner.Api.Properties.Controllers;

public class ReportController : ControllerBase
{
    // GET
    public IActionResult Index()
    {
        return View();
    }
}
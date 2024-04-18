using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace ProjectsManager.Controllers
{
	public class MainController : Controller
	{
		private readonly ILogger<MainController> _logger;

		public MainController(ILogger<MainController> logger)
		{
			_logger = logger;
		}

		public IActionResult MainPage()
		{
			return View();
		}
	}
}

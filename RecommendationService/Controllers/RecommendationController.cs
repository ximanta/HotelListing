using Microsoft.AspNetCore.Mvc;

namespace RecommendationService.Controllers
{
    public class RecommendationController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}

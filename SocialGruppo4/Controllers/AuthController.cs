using Microsoft.AspNetCore.Mvc;

namespace SocialGruppo4.Controllers
{
    public class AuthController : Controller
    {
        [HttpGet("accedi")]
        public IActionResult Index()
        {
            return View();
        }
    }
}
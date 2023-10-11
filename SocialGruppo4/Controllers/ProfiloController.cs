using Microsoft.AspNetCore.Mvc;

namespace SocialGruppo4.Controllers
{
    public class ProfiloController : Controller
    {
        public IActionResult Index()
        {
            if (HttpContext.Items["User"] is null)
            {
                return RedirectToAction("Index","Auth");
            }

            return View();
        }
    }
}

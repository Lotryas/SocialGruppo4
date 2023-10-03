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

        [HttpPost("accedi")]
        public IActionResult Login()
        {
            string email = Request.Form["email"];
            string plainPassword = Request.Form["password"];

            CookieOptions cookieOpts = new()
            {
                Expires = DateTime.Now.AddYears(1),
                HttpOnly = true,
                Secure = true,
                SameSite = SameSiteMode.Lax
            };

            Response.Cookies.Append("auth", email, cookieOpts);

            return Redirect("/");
        }
    }
}
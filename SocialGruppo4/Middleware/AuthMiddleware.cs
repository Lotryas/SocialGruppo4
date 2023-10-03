using SocialGruppo4.Models.Utenti;

namespace SocialGruppo4.Middleware
{
    public class AuthMiddleware
    {
        private readonly RequestDelegate _next;

        public AuthMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            var idUtente = context.Request.Cookies["auth"];

            if (idUtente is not null)
            {
                int id = int.Parse(idUtente);
                Utente? utente = (Utente?)DAOUtenti.GetInstance().Find(id);
                context.Items["User"] = utente;
            }
            else
            {
                context.Items["User"] = null;
            }

            await _next(context);
        }
    }

    public static class RequestCultureMiddlewareExtensions
    {
        public static IApplicationBuilder UseAuthMiddleware(
            this IApplicationBuilder builder)
        {
            return builder.UseMiddleware<AuthMiddleware>();
        }
    }
}
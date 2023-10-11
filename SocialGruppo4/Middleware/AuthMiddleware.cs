using SocialGruppo4.Models.Utenti;

namespace SocialGruppo4.Middleware
{
    // DOC: Questo middleware serve ad identificare l'utente ad ogni request
    public class AuthMiddleware
    {
        private readonly RequestDelegate _next;

        public AuthMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        // DOC: Questo metodo viene chiamato su ogni richiesta e grazie al
        // al suo oggetto HttpContext accede ai cookie e controlla se
        // se l'utente ha già effettuato l'accesso, in quel caso lo recupera
        // dal database e lo salva negli Items del context.
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

    // DOC: Registra il middleware per poter essere chiamato tramite app.UseAuthMiddleware()
    public static class RequestCultureMiddlewareExtensions
    {
        public static IApplicationBuilder UseAuthMiddleware(
            this IApplicationBuilder builder)
        {
            return builder.UseMiddleware<AuthMiddleware>();
        }
    }
}
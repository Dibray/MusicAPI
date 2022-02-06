namespace Music.API
{
    using Microsoft.AspNetCore.Http;
    using Microsoft.EntityFrameworkCore;
    using System.Linq;

    using static Account;

    public class Authorization
    {
        private readonly RequestDelegate _next;

        public Authorization(RequestDelegate next)
        {
            this._next = next;
        }

        public async System.Threading.Tasks.Task InvokeAsync(HttpContext context)
        {
            string authToken = context.Request.Cookies[AUTH_TOKEN];

            if (authToken == null)
            { 
                Role = Models.Role.Guest;
                await _next.Invoke(context);
                return;
            }

            Database.MusicContext db = new Database.MusicContext();

            string login = context.Request.Cookies[USER_ID];

            // Get corresponding user from database
            Models.User.Db user =
                await db.Users.Include(u => u.Login).Where(u => u.Login.Value == login).FirstOrDefaultAsync();

            if (authToken == user.AuthToken)
                Role = user.Role;

            await _next.Invoke(context);
        }
    }
}

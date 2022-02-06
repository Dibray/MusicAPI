namespace Music.API
{
    using Microsoft.AspNetCore.Mvc;
    using System.Threading.Tasks;

    using Music.Models;

    [ApiController]
    [Route("api/[controller]")]
    public class Account : ControllerBase
    {
        internal static Role Role { get; set; } = Role.Guest;

        internal const string AUTH_TOKEN = "AuthToken";
        internal const string USER_ID = "UserId";

        [HttpPost]
        public async Task<JsonResult> Register(string login, string password, string nickname = null,
            int? birthYear = null)
        {
            if (Role != Role.Guest)
                return new JsonResult(false);

            try
            {
                Login newLogin = new Login(login);
                Password newPassword = new Password(password);

                bool success = await Models.User.Register(newLogin, newPassword, nickname, birthYear);

                return new JsonResult(success);
            }
            catch (System.ArgumentException)
            {
                return new JsonResult(false);
            }
        }

        [HttpGet]
        public async Task<JsonResult> LogIn(string login, string password)
        {
            if (Role != Role.Guest)
                return new JsonResult(false);

            try
            {
                Login checkLogin = new Login(login);
                Password checkPassword = new Password(password);

                string authToken = await Models.User.LogIn(checkLogin, checkPassword);

                if (authToken == null)
                    return new JsonResult(false); // Incorrect login or password

                Response.Cookies.Delete(AUTH_TOKEN);
                Response.Cookies.Append(AUTH_TOKEN, authToken);
                Response.Cookies.Delete(USER_ID);
                Response.Cookies.Append(USER_ID, login);

                return new JsonResult(true);
            }
            catch (System.ArgumentException)
            {
                return new JsonResult(false);
            }
        }
    }
}

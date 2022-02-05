namespace Music.API
{
    using Microsoft.AspNetCore.Mvc;
    using System.Threading.Tasks;

    using Music.Models;

    [ApiController]
    [Route("api/[controller]")]
    public class Account : ControllerBase
    {
        private static Role Role { get; set; } = Role.Guest;

        private const string AUTH_TOKEN = "AuthToken";
        private const string USER_ID = "UserId";

        [HttpPost]
        public async Task<JsonResult> Register(string login, string password, string nickname = null,
            int? birthYear = null)
        {
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
        public async Task<bool> LogIn(string login, string password)
        {
            try
            {
                Login checkLogin = new Login(login);
                Password checkPassword = new Password(password);

                string authToken = await Models.User.LogIn(checkLogin, checkPassword);

                if (authToken == null)
                    return false; // Incorrect login or password

                Response.Cookies.Delete(AUTH_TOKEN);
                Response.Cookies.Append(AUTH_TOKEN, authToken);
                Response.Cookies.Delete(USER_ID);
                Response.Cookies.Append(USER_ID, login);

                return true;
            }
            catch (System.ArgumentException)
            {
                return false;
            }
        }
    }
}

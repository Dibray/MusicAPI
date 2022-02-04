namespace Music.API
{
    using Microsoft.AspNetCore.Mvc;
    using System.Threading.Tasks;

    using Music.Models;

    [ApiController]
    [Route("api/[controller]")]
    public class Account : ControllerBase
    {
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
    }
}

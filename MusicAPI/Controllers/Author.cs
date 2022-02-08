namespace Music.API
{
    using Microsoft.AspNetCore.Mvc;
    using System.Threading.Tasks;

    using Models;

    [ApiController]
    [Route("api/[controller]")]
    public class Author : ControllerBase
    {
        [HttpPost]
        public async Task<JsonResult> NewAuthor(System.DateTime birthDate, string firstName = "", string lastName = "")
        {
            if (Account.Role == Models.Role.Guest)
                return new JsonResult(false);

            FullName name = new FullName(firstName, lastName);
            Models.Author author = new Models.Author(name, birthDate);

            if (await Models.Author.NewAuthor(author))
                return new JsonResult(true);

            return new JsonResult(false);
        }
    }
}

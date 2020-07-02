using Microsoft.AspNetCore.Mvc;

namespace FootballInformationSystem.Api.Controllers
{
    [ApiController]
    public class ErrorController : Controller
    {
        [Route("/error")]
        public IActionResult Error() => Problem();
    }
}

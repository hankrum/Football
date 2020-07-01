using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace FootballInformationSystem.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class GamesController : Controller
    {


        public IActionResult Index()
        {
            return View();
        }
    }
}

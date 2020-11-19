using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace TitleParsingTask.Controllers
{
    public class HomeController : Controller
    {
        
        public HomeController()
        {

        }

        [HttpGet("")]
        public IActionResult Index()
        {
            return View();
        }

    }
}

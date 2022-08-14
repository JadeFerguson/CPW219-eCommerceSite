using Microsoft.AspNetCore.Mvc;

namespace CPW219_eCommerceSite.Controllers
{
    public class GamesController : Controller
    {
        [HttpGet]
        // To add razor view right click on create then will show up
        public IActionResult Create()
        {
            
            return View();
        }
    }
}

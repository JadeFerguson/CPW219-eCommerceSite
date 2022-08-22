using Microsoft.AspNetCore.Mvc;

namespace CPW219_eCommerceSite.Controllers
{
    public class MembersController : Controller
    {
        // added this in controllers and is a Mvc empty
        public IActionResult Register()
        {
            return View();
        }
    }
}

using CPW219_eCommerceSite.Data;
using CPW219_eCommerceSite.Models;
using Microsoft.AspNetCore.Mvc;

namespace CPW219_eCommerceSite.Controllers
{
    public class MembersController : Controller
    {
        private readonly VideoGameContext _context;

        // controller do ctor tab tab
        // database context to access anywhere in this controller
        public MembersController(VideoGameContext context)
        {
            _context = context;
        }
        // added this in controllers and is a Mvc empty
        [HttpGet]
        public IActionResult Register()
        {
            return View();
        }

        [HttpPost] //when send info and make sure you are using the proper model
        public async Task<IActionResult> Register(RegisterViewModel regmModel)
        {
            if (ModelState.IsValid)
            {
                // Map registerViewModel data to member object
                Member newMember = new()
                {
                    Email = regmModel.Email,
                    Password = regmModel.Password

                };

                _context.Members.Add(newMember);
               await  _context.SaveChangesAsync();

                // REdirect to home page
                //index of the home controller
                return RedirectToAction("Index", "Home");

            }
            return View(regmModel);
        }
    }
}
